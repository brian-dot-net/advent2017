namespace Advent
{
    using System.Linq;

    internal sealed class OneBitsTable
    {
        private readonly int[] table;

        public OneBitsTable()
        {
            this.table = Enumerable.Range(0, 256).Select(b => OneBits((byte)b)).ToArray();
        }

        public int this[byte b] => this.table[b];

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