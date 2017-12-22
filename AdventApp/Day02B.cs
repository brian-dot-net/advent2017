namespace Advent
{
    using System;
    using System.Linq;

    public sealed class Day02B : Day02
    {
        protected override int RunCore(Input input) => Rows(input).Select(r => DivideTwo(r)).Sum();

        private static int DivideTwo(int[] row)
        {
            Array.Sort(row, (a, b) => b.CompareTo(a));
            for (int i = 0; i < row.Length - 1; ++i)
            {
                int x = row[i];
                for (int j = i + 1; j < row.Length; ++j)
                {
                    int y = row[j];
                    if (x % y == 0)
                    {
                        return x / y;
                    }
                }
            }

            throw new InvalidOperationException("Unexpected!");
        }
    }
}