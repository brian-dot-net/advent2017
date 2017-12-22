namespace Advent
{
    using System.Linq;

    public sealed class Day02A : Day02
    {
        protected override int RunCore(string input) => Rows(input).Select(MaxDiff).Sum();

        private static int MaxDiff(int[] values)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (int v in values)
            {
                if (v < min)
                {
                    min = v;
                }

                if (v > max)
                {
                    max = v;
                }
            }

            return max - min;
        }
    }
}