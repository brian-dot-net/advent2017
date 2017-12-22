namespace Advent
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Day02 : DayBase<int>
    {
        protected static IEnumerable<int[]> Rows(Input input) => input.Lines().Select(l => l.Fields()).Select(r => AsInts(r));

        protected static int[] AsInts(Input[] values)
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