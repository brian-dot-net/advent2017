namespace Advent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Day3 : ICanRun
    {
        public string DefaultInput
        {
            get => @"265149";
        }

        public int Run(string input)
        {
            return this.RunCore(input);
        }

        protected abstract int RunCore(string input);

        protected sealed class Ring
        {
            private readonly Ring previous;
            private readonly int radius;
            private readonly int[] values;

            private Ring(Ring previous, int radius)
            {
                this.previous = previous;
                this.radius = radius;
                int n = 1;
                if (this.Count > 0)
                {
                    n = this.Count;
                }

                this.values = Enumerable.Range(this.PrevMax + 1, n).ToArray();
            }

            public int Max => this.values[this.values.Length - 1];

            private int Min => this.values[0];

            private int Count => 8 * this.radius;

            private int PrevMax
            {
                get
                {
                    if (this.previous == null)
                    {
                        return 0;
                    }

                    return this.previous.Max;
                }
            }

            public static Ring First()
            {
                return new Ring(null, 0);
            }

            public Ring Next()
            {
                return new Ring(this, this.radius + 1);
            }

            public int Distance(int n)
            {
                int i = n - this.Min;
                Pair p = this.Coords().Skip(i).First();
                return p.Distance;
            }

            private static Pair P(int x, int y) => new Pair(x, y);

            private IEnumerable<Pair> Coords()
            {
                Spiral spiral = new Spiral(this.radius);
                int n = this.Count;
                do
                {
                    yield return spiral.Current;
                    spiral.MoveNext();
                    --n;
                }
                while (n > 0);
            }

            private struct Pair
            {
                private readonly int x;
                private readonly int y;

                public Pair(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }

                public int Distance => this.DX + this.DY;

                private int DX => Math.Abs(this.x);

                private int DY => Math.Abs(this.y);

                public static Pair operator +(Pair a, Pair b) => new Pair(a.x + b.x, a.y + b.y);

                public bool Exceeds(int r)
                {
                    return (this.DX > r) || (this.DY > r);
                }
            }

            private sealed class Spiral
            {
                private readonly int radius;

                private int dir;

                public Spiral(int radius)
                {
                    this.radius = radius;
                    if (this.radius > 0)
                    {
                        this.Current = new Pair(this.radius, 1 - this.radius);
                    }
                }

                public Pair Current { get; private set; }

                private Pair Dir
                {
                    get
                    {
                        switch (this.dir)
                        {
                            case 1: return P(-1, 0);
                            case 2: return P(0, -1);
                            case 3: return P(1, 0);
                            default: return P(0, 1);
                        }
                    }
                }

                public void MoveNext()
                {
                    Pair next = this.TrialMove();
                    if (next.Exceeds(radius))
                    {
                        this.NextDir();
                        next = this.TrialMove();
                    }

                    this.Current = next;
                }

                private Pair TrialMove()
                {
                    return this.Current + this.Dir;
                }

                private void NextDir()
                {
                    this.dir = (this.dir + 1) % 4;
                }
            }
        }
    }
}