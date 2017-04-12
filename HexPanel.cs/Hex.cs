using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexPanel.cs
{
    public class Hex
    {
        public static Hex Zero { get; } = new Hex(0, 0, 0);

        public readonly int Q;

        public readonly int R;

        public readonly int S;

        public Hex(int q, int r) : this(q, r, -q - r)
        {
        }

        public Hex(int q, int r, int s)
        {
            if (q + r + s != 0) throw new ArgumentException();
            Q = q;
            R = r;
            S = s;
        }

        public override bool Equals(object obj)
        {
            return obj is Hex && this == (Hex)obj;
        }

        public override int GetHashCode()
        {
            return Q.GetHashCode() ^ R.GetHashCode() ^ S.GetHashCode();
        }

        public static bool operator ==(Hex l, Hex r)
        {
            return (l.Q == r.Q && l.R == r.R && l.S == r.S);
        }

        public static bool operator !=(Hex l, Hex r)
        {
            return !(l == r);
        }

        public static Hex FromOffset(Offset offset)
        {
            int q = offset.Col - ((offset.Row + 1 * (offset.Row & 1)) / 2);
            int r = offset.Row;

            return new Hex(q, r);
        }
    }
}
