﻿namespace Advent.Day01
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected static int Matching(char a, char b) => (a == b) ? (a - '0') : 0;

        protected static IEnumerable<T> Rotate<T>(IEnumerable<T> input, int n) => input.Skip(n).Concat(input.Take(n));
    }
}