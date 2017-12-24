namespace Advent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Day15 : DayBase<int>
    {
        protected sealed class Generators
        {
            private static readonly int[] factors = new int[] { 16807, 48271 };

            private readonly Generator[] generators;

            public Generators(Input input)
            {
                this.generators = 
                    input.Lines()
                    .Select(l => l.Fields(" starts with ")[1].Integer())
                    .Zip(factors, (s, f) => new Generator(s, f))
                    .ToArray();
            }

            public IEnumerable<int> this[int i] => this.generators[i].Sequence();

            public int Run() => this[0].Zip(this[1], Match).Take(40000000).Sum();

            private static int Match(int a, int b) => (a & 0xFFFF) == (b & 0xFFFF) ? 1 : 0;

            private sealed class Generator
            {
                private readonly int start;
                private readonly int factor;

                public Generator(int start, int factor)
                {
                    this.start = start;
                    this.factor = factor;
                }

                public IEnumerable<int> Sequence()
                {
                    int previous = this.start;
                    while (true)
                    {
                        yield return previous = (int)(Math.BigMul(previous, this.factor) % int.MaxValue);                        
                    }
                }
            }
        }
    }
}