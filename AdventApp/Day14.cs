namespace Advent
{
    using System.Linq;

    public abstract class Day14 : DayBase<int>
    {
        protected sealed class Disk
        {
            private readonly GridRow[] rows;

            public Disk(Input input)
            {
                this.rows = new GridRow[128];
                Knot knot = new Knot(256);

                for (int i = 0; i < this.rows.Length; ++i)
                {
                    this.rows[i] = new GridRow(new Input(input + "-" + i));
                }
            }

            public int UsedSquares() => this.rows.Select(r => r.UsedSquares()).Sum();

            private struct GridRow
            {
                private readonly byte[] row;

                public GridRow(Input input)
                {
                    this.row = Knot.Hash(input);
                }

                public int UsedSquares() => this.row.Select(b => OneBits(b)).Sum();

                private static int OneBits(byte b)
                {
                    int ones = 0;
                    while (b > 0)
                    {
                        ones += b & 1;
                        b >>= 1;
                    }

                    return ones;
                }
            }
        }
    }
}