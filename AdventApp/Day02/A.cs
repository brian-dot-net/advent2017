namespace Advent.Day02
{
    using System.Linq;

    public sealed class A : Base
    {
        protected override int RunCore(Input input) => Rows(input).Select(MaxDiff).Sum();

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