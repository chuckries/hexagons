using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using static System.Math;

namespace HexPanel.cs
{
    public class Layout
    {
        public readonly Orientation Orientation;
        public readonly Size Size;
        public readonly Point Origin;

        public Layout(Orientation orientation, Size size, Point origin)
        {
            Orientation = orientation;
            Size = size;
            Origin = origin;
        }

        public Point HexCenter(Hex hex)
        {
            Orientation o = Orientation;
            double x = (o.F0 * hex.Q + o.F1 * hex.R) * Size.Width;
            double y = (o.F2 * hex.Q + o.F3 * hex.R) * Size.Height;
            return new Point(x + Origin.X, y + Origin.Y);
        }

        public Point HexCornerOffset(int corner)
        {
            double angle = 2 * PI * (Orientation.StartAngle + corner) / 6;
            return new Point(Size.Width * Cos(angle), Size.Height * Sin(angle));
        }

        public List<Point> HexCorners(Hex hex)
        {
            List<Point> points = new List<Point>(6);
            Point center = HexCenter(hex);
            for (int i = 0; i < 6; i ++)
            {
                Point offset = HexCornerOffset(i);
                points.Add(new Point(center.X + offset.X, center.Y + offset.Y));
            }
            return points;
        }

        // Get's the bounding rect
        public Rect HexRect(Hex hex)
        {
            List<Point> points = HexCorners(hex);

            double left = double.PositiveInfinity;
            double right = double.NegativeInfinity;
            double top = double.PositiveInfinity;
            double bottom = double.NegativeInfinity;

            foreach (Point point in points)
            {
                left = Min(left, point.X);
                right = Max(right, point.X);
                top = Min(top, point.Y);
                bottom = Max(bottom, point.Y);
            }

            return new Rect(new Point(left, top), new Point(right, bottom));
        }

        /// <summary>
        /// Gets the rect that is completely contained by the hex
        /// </summary>
        public Rect MinHexRect(Hex hex)
        {
            List<Point> corners = HexCorners(hex);

            return new Rect(corners[3], corners[0]);
        }
    }
}
