using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static System.Math;

namespace HexPanel.cs
{
    public class HexPanel : Panel
    {
        public static readonly DependencyProperty QProperty =
            DependencyProperty.RegisterAttached("Q", typeof(int), typeof(HexPanel),
                new PropertyMetadata(0));

        public static readonly DependencyProperty RProperty =
            DependencyProperty.RegisterAttached("R", typeof(int), typeof(HexPanel),
                new PropertyMetadata(0));

        public static readonly DependencyProperty HexSizeProperty =
            DependencyProperty.Register(nameof(HexSize), typeof(Size), typeof(HexPanel),
                new PropertyMetadata(new Size(50, 50)));

        public static void SetQ(UIElement element, int q)
        {
            element.SetValue(QProperty, q);
        }

        public static int GetQ(UIElement element)
        {
            return (int)element.GetValue(QProperty);
        }

        public static void SetR(UIElement element, int r)
        {
            element.SetValue(RProperty, r);
        }

        public static int GetR(UIElement element)
        {
            return (int)element.GetValue(RProperty);
        }

        public Size HexSize
        {
            get { return (Size)GetValue(HexSizeProperty); }
            set { SetValue(HexSizeProperty, value); }
        }

        public HexPanel()
        {
        }

        private Rect _totalRect;
        private Size _size;

        protected override Size MeasureOverride(Size availableSize)
        {
            Point origin = new Point(0, 0);
            Layout layout = new Layout(Orientation.Pointy, HexSize, origin);

            Rect minRect = layout.MinHexRect(Hex.Zero);
            _size = new Size(minRect.Width, minRect.Height);

            double left = double.PositiveInfinity;
            double right = double.NegativeInfinity;
            double top = double.PositiveInfinity;
            double bottom = double.NegativeInfinity;

            foreach (var child in Children)
            {
                child.Measure(_size);

                Hex hex = new Hex(GetQ(child), GetR(child));

                Rect rect = layout.HexRect(hex);

                left = Min(left, rect.Left);
                right = Max(right, rect.Right);
                top = Min(top, rect.Top);
                bottom = Max(bottom, rect.Bottom);
            }

            _totalRect = new Rect(new Point(left, top), new Point(right, bottom));
            return new Size(_totalRect.Width, _totalRect.Height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Point origin = new Point(-_totalRect.Left, -_totalRect.Top);
            Layout layout = new Layout(Orientation.Pointy, HexSize, origin);

            foreach (UIElement child in Children)
            {
                Hex hex = new Hex(GetQ(child), GetR(child));
                Rect rect = layout.MinHexRect(hex);
                //rect = new Rect(rect.X + child.DesiredSize.Width / 4, rect.Y, rect.Width, rect.Height);

                child.Arrange(rect);
            }

            return new Size(_totalRect.Width, _totalRect.Height);
        }
    }
}
