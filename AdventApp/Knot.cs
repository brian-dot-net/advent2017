namespace Advent
{
    using System.Linq;

    internal sealed class Knot
    {
        private readonly int length;

        public Knot(int length)
        {
            this.length = length;
        }

        public static byte[] Hash(Input input)
        {
            byte[] bytes = input.AsciiBytes().Concat(new byte[] { 17, 31, 73, 47, 23 }).ToArray();
            byte[] result = new Knot(256).Hash(64, bytes);
            return Compact(result);
        }

        public byte[] Hash(int rounds, byte[] bytes)
        {
            byte[] result = Enumerable.Range(0, this.length).Select(b => (byte)b).ToArray();
            int skip = 0;
            int i = 0;
            for (int k = 0; k < rounds; ++k)
            {
                foreach (int r in bytes)
                {
                    Reverse(result, i, r);
                    i = (i + r + skip) % result.Length;
                    ++skip;
                }
            }

            return result;
        }

        private static void Reverse(byte[] result, int start, int length)
        {
            for (int i = 0; i < length / 2; ++i)
            {
                Swap(result, start + i, start + length - i - 1);
            }
        }

        private static void Swap(byte[] result, int i, int j)
        {
            int n = result.Length;
            i = i % n;
            j = j % n;
            byte t = result[i];
            result[i] = result[j];
            result[j] = t;
        }

        private static byte[] Compact(byte[] input)
        {
            byte[] result = new byte[16];
            for (int i = 0; i < 16; ++i)
            {
                result[i] = HashOne(input, i * 16);
            }

            return result;
        }

        private static byte HashOne(byte[] input, int start)
        {
            byte result = input[start];
            for (int i = 1; i < 16; ++i)
            {
                result ^= input[start + i];
            }

            return result;
        }
    }
}