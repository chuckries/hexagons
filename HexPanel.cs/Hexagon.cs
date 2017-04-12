using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace HexPanel.cs
{
    public class Hexagon : Path
    {
        public Hexagon()
        {
            RegisterPropertyChangedCallback(WidthProperty, PathChangingPropertyChanged);
            SizeChanged += Hexagon_SizeChanged;
        }

        private void PathChangingPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            InvalidateMeasure();
        }

        private void Hexagon_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetPathData();
        }

        private void SetPathData()
        {
            Point center = new Point(ActualWidth / 2, ActualHeight / 2);
            double offset = StrokeThickness / 2;
            Size innerSize = new Size(ActualWidth - offset * 2, ActualHeight - offset * 2);
            Rect innerRect = new Rect(new Point(offset, offset), innerSize);

            Point[] points = new Point[6];
            points[0] = new Point(innerRect.Left, innerRect.Top + innerRect.Height * 0.25);
            points[1] = new Point(center.X, innerRect.Top);
            points[2] = new Point(innerRect.Right, innerRect.Top + innerRect.Height * 0.25);
            points[3] = new Point(innerRect.Right, innerRect.Bottom - 0.25 * innerRect.Height);
            points[4] = new Point(center.X, innerRect.Bottom);
            points[5] = new Point(innerRect.Left, innerRect.Bottom - 0.25 * innerRect.Height);

            PathFigure figure = new PathFigure()
            {
                StartPoint = points[0],
                IsClosed = true,
                IsFilled = true,
                Segments = new PathSegmentCollection
                {
                    new LineSegment { Point = points[1] },
                    new LineSegment { Point = points[2] },
                    new LineSegment { Point = points[3] },
                    new LineSegment { Point = points[4] },
                    new LineSegment { Point = points[5] }
                }
            };
            Data = new PathGeometry
            {
                Figures = new PathFigureCollection
                {
                    figure
                }
            };
        }
    }
}
