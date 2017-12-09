﻿namespace Advent
{
    using System.Linq;

    public sealed class Day2A : Day2
    {
        protected override int RunCore(string input)
        {
            return Rows(input).Select(MaxDiff).Sum();
        }

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