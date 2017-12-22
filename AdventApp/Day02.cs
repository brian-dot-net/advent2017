namespace Advent
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Day02 : DayBase<int>
    {
        protected static IEnumerable<int[]> Rows(Input input) => input.Lines().Select(l => l.Split()).Select(r => AsInts(r));

        protected static int[] AsInts(string[] values)
        {
            int[] results = new int[values.Length];
            for (int i = 0; i < values.Length; ++i)
            {
                results[i] = int.Parse(values[i]);
            }

            return results;
        }
    }
}