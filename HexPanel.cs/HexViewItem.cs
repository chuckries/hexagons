using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HexPanel.cs
{
    public class HexViewItem : ContentControl
    {
        public static readonly DependencyProperty HexMarginProperty =
            DependencyProperty.Register(nameof(HexMargin), typeof(Thickness), typeof(HexViewItem),
                new PropertyMetadata(new Thickness(0)));
        public Thickness HexMargin
        {
            get { return (Thickness)GetValue(HexMarginProperty); }
            set { SetValue(HexMarginProperty, value); }
        }

        Hexagon _backgroundHex;
        Hexagon _accentHex;
        Hexagon _focusHex;

        bool _isPointerOver;

        public HexViewItem()
        {
            SizeChanged += HexViewItem_SizeChanged;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _backgroundHex = (Hexagon)GetTemplateChild("BackgroundHex");
            _accentHex = (Hexagon)GetTemplateChild("AccentHex");
            _focusHex = (Hexagon)GetTemplateChild("FocusHex");

            if (_focusHex != null)
            {
                _focusHex.PointerEntered += _hexagon_PointerEntered;
                _focusHex.PointerExited += _hexagon_PointerExited;
            }
        }

        private void _hexagon_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType != Windows.Devices.Input.PointerDeviceType.Touch)
            {
                _isPointerOver = true;
                UpdateForVisualState();
            }
        }

        private void _hexagon_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType != Windows.Devices.Input.PointerDeviceType.Touch)
            {
                _isPointerOver = false;
                UpdateForVisualState();
            }
        }

        private void UpdateForVisualState()
        {
            string state = "Normal";
            if (_isPointerOver)
            {
                state = "PointerOver";
            }

            VisualStateManager.GoToState(this, state, false);
        }

        private void HexViewItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeHexagon(_backgroundHex);
            ResizeHexagon(_accentHex);
            ResizeHexagon(_focusHex);
        }

        private void ResizeHexagon(Hexagon hex)
        {
            if (hex == null) return;
            hex.Width = ActualWidth - HexMargin.Left - HexMargin.Right;
            hex.Height = ActualHeight * 2 - HexMargin.Top - HexMargin.Bottom;
            hex.Margin = new Thickness(HexMargin.Left, -ActualHeight / 2 + HexMargin.Top, HexMargin.Right, -ActualHeight / 2 + HexMargin.Bottom);
        }
    }
}
