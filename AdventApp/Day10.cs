namespace Advent
{
    using System.Linq;

    public abstract class Day10 : ICanRun
    {
        public string DefaultInput => "256:106,16,254,226,55,2,1,166,177,247,93,0,255,228,60,36";

        public string Run(string input) => this.RunCore(input).ToString();

        protected abstract int RunCore(string input);

        protected sealed class Knot
        {
            private readonly int[] list;
            private readonly int[] lengths;

            public Knot(string input)
            {
                string[] fields = input.Split(':');
                this.list = Enumerable.Range(0, int.Parse(fields[0])).ToArray();
                this.lengths = fields[1].Split(',').Select(int.Parse).ToArray();
            }

            public int Hash()
            {
                int skip = 0;
                int i = 0;
                foreach (int r in this.lengths)
                {
                    this.Reverse(i, r);
                    i = (i + r + skip) % this.list.Length;
                    ++skip;
                }

                return this.list[0] * this.list[1];
            }

            private void Reverse(int start, int length)
            {
                for (int i = 0; i < length / 2; ++i)
                {
                    this.Swap(start + i, start + length - i - 1);
                }
            }

            private void Swap(int i, int j)
            {
                int n = this.list.Length;
                i = i % n;
                j = j % n;
                int t = this.list[i];
                this.list[i] = this.list[j];
                this.list[j] = t;
            }
        }
    }
}