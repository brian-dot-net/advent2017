namespace Advent
{
    using System.Linq;

    public abstract class Day10 : DayBase<string>
    {
        public override string DefaultInput => null;

        protected sealed class Knot
        {
            private readonly int length;

            public Knot(int length)
            {
                this.length = length;
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
        }
    }
}