namespace Advent
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public sealed class Day2A : Day2
    {
        protected override int RunCore(string input)
        {
            return Rows(input).Select(MaxDiff).Sum();
        }

        private static int MaxDiff(IEnumerable<int> values)
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

        private static IEnumerable<IEnumerable<int>> Rows(string input)
        {
            return LinesOf(input).Select(l => l.Split()).Select(r => AsInts(r));
        }

        private static IEnumerable<int> AsInts(IEnumerable<string> values)
        {
            return values.Select(v => int.Parse(v));
        }

        private static IEnumerable<string> LinesOf(string input)
        {
            using (StringReader sr = new StringReader(input))
            {
                string next;
                do
                {
                    next = sr.ReadLine();
                    if (next != null)
                    {
                        yield return next;
                    }
                }
                while (next != null);
            }
        }
    }
}