namespace Advent.Day02
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected static int MaxDiff(int[] values)
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

        protected static int DivideTwo(int[] row)
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

        protected sealed class Rows
        {
            private readonly IEnumerable<int[]> rows;

            public Rows(Input input)
            {
                this.rows = input.Lines().Select(l => l.Fields()).Select(r => AsInts(r));
            }

            public int SumOf(Func<int[], int> reduce) => this.rows.Select(reduce).Sum();

            private static int[] AsInts(Input[] values)
            {
                int[] results = new int[values.Length];
                for (int i = 0; i < values.Length; ++i)
                {
                    results[i] = values[i].Integer();
                }

                return results;
            }
        }
    }
}