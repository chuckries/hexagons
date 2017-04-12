using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using static System.Math;

namespace HexPanel.cs
{
    public static class HexExtensions
    {
        public static Point RoundPoint(this Point point)
        {
            return new Point(Round(point.X, MidpointRounding.AwayFromZero), Round(point.Y, MidpointRounding.AwayFromZero));
        }

        public static Rect RoundRect(this Rect rect)
        {
            return new Rect(new Point(Round(rect.Left, MidpointRounding.AwayFromZero), Round(rect.Top, MidpointRounding.AwayFromZero)), 
                new Point(Round(rect.Right, MidpointRounding.AwayFromZero), Round(rect.Bottom, MidpointRounding.AwayFromZero)));
        }
    }
}
