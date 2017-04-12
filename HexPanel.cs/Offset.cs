using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexPanel.cs
{
    public class Offset
    {
        public readonly int Col, Row;

        public Offset(int col, int row)
        {
            Col = col;
            Row = row;
        }

        public static Offset FromHex(Hex hex)
        {
            int col = hex.Q;
            int row = hex.R + ((hex.Q + 1 * (hex.Q & 1)) / 2);
            return new Offset(col, row);
        }
    }
}
