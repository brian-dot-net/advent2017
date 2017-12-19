﻿namespace Advent
{
    using System.Linq;

    public sealed class Day3B : Day3
    {
        protected override int RunCore(string input)
        {
            int n = int.Parse(input);
            Spiral spiral = new Spiral();
            return spiral.Cells(true).First(c => c.Value > n).Value;
        }
    }
}