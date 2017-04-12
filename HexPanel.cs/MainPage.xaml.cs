using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HexPanel.cs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Random random = new Random();

        public MainPage()
        {
            this.InitializeComponent();

            int row = 2;
            int col = 2;

            //for (int i = row; i >= -row; i--)
            //{
            //    for (int j = col; j >= -col; j--)
            //    {
            //        //Hexagon hexagon = new Hexagon { Fill = new SolidColorBrush(color), Stroke = new SolidColorBrush(Colors.Transparent) };

            //        HexViewItem hexagon = new HexViewItem { Content = $"{i},{j}" };
            //        HexPanel.SetQ(hexagon, i);
            //        HexPanel.SetR(hexagon, j);
            //        MyHexPanel.Children.Add(hexagon);
            //    }
            //}

            int radius = 10;
            for (int q = -radius; q <= radius; q++)
            {
                int r1 = Math.Max(-radius, -q - radius);
                int r2 = Math.Min(radius, -q + radius);
                for (int r = r1; r <= r2; r++)
                {
                    HexViewItem hexagon = new HexViewItem { Content = $"{q},{r}" };
                    HexPanel.SetQ(hexagon, q);
                    HexPanel.SetR(hexagon, r);
                    MyHexPanel.Children.Add(hexagon);
                }
            }
        }

        private void ScrollViewer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            ScrollToCenter((ScrollViewer)sender);
        }

        private void ScrollViewer_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            PointerTextBlock.Text = e.OriginalSource.ToString();
        }

        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollViewer viewer = (ScrollViewer)sender;
            ScrollToCenter(viewer);
        }

        private void ScrollToCenter(ScrollViewer viewer)
        {
            double width = (viewer.ExtentWidth / viewer.ZoomFactor)  / 2 - viewer.ActualWidth / 2;
            double height = (viewer.ExtentHeight / viewer.ZoomFactor) / 2 - viewer.ActualHeight / 2;
            float zoom = 1;
            viewer.ChangeView(width, height, zoom);
        }

        private Color GetRandomColor()
        {
            byte[] bytes = new byte[3];
            random.NextBytes(bytes);
            return Color.FromArgb(0xFF, bytes[0], bytes[1], bytes[2]);
        }
    }
}
