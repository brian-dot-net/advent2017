namespace Advent.Day03
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        private static Pair P(int x, int y) => new Pair(x, y);

        protected sealed class Spiral
        {
            private readonly bool useSums;

            public Spiral(bool useSums)
            {
                this.useSums = useSums;
            }

            public Cell FirstCell(Func<Cell, bool> pred) => this.Rings().SelectMany(r => r.Cells()).First(pred);

            private IEnumerable<Ring> Rings()
            {
                Ring current = Ring.First(this.useSums);
                while (true)
                {
                    yield return current;
                    current = current.Next();
                }
            }

            private sealed class Ring
            {
                private readonly CellValues values;
                private readonly int radius;

                private Ring(CellValues values, int radius)
                {
                    this.values = values;
                    this.radius = radius;
                }

                private int Count => 8 * this.radius;

                public static Ring First(bool useSums) => new Ring(CellValues.New(useSums), 0);

                public Ring Next() => new Ring(this.values, this.radius + 1);

                public IEnumerable<Cell> Cells() => new RingCells(values, this.radius).All();

                private abstract class CellValues
                {
                    protected CellValues()
                    {
                    }

                    public static CellValues New(bool useSums)
                    {
                        if (useSums)
                        {
                            return new AdjacentSumCellValues();
                        }

                        return new SequentialCellValues();
                    }

                    public Cell Get(Pair pair) => new Cell(pair, this.GetValue(pair));

                    protected abstract int GetValue(Pair pair);

                    private sealed class SequentialCellValues : CellValues
                    {
                        private int value;

                        protected override int GetValue(Pair pair) => ++this.value;
                    }

                    private sealed class AdjacentSumCellValues : CellValues
                    {
                        private readonly Dictionary<Pair, int> cache;

                        public AdjacentSumCellValues()
                        {
                            this.cache = new Dictionary<Pair, int>();
                        }

                        protected override int GetValue(Pair pair)
                        {
                            int sum = 0;

                            sum += this.CachedValue(pair + P(1, 0));
                            sum += this.CachedValue(pair + P(1, 1));
                            sum += this.CachedValue(pair + P(0, 1));
                            sum += this.CachedValue(pair + P(-1, 1));
                            sum += this.CachedValue(pair + P(-1, 0));
                            sum += this.CachedValue(pair + P(-1, -1));
                            sum += this.CachedValue(pair + P(0, -1));
                            sum += this.CachedValue(pair + P(1, -1));

                            if (sum == 0)
                            {
                                sum = 1;
                            }

                            this.cache.Add(pair, sum);
                            return sum;
                        }

                        private int CachedValue(Pair p)
                        {
                            int v;
                            if (!this.cache.TryGetValue(p, out v))
                            {
                                v = 0;
                            }

                            return v;
                        }
                    }
                }

                private sealed class RingCells
                {
                    private readonly CellValues values;
                    private readonly int radius;

                    private Pair current;
                    private int dir;

                    public RingCells(CellValues values, int radius)
                    {
                        this.values = values;
                        this.radius = radius;
                        if (this.radius > 0)
                        {
                            this.current = P(this.radius, 1 - this.radius);
                        }
                    }

                    private int Count => this.radius * 8;

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

                    public IEnumerable<Cell> All()
                    {
                        int i = 0;
                        do
                        {
                            yield return this.values.Get(this.current);
                            this.current = this.Next();
                            ++i;
                        }
                        while (i < this.Count);
                    }

                    private Pair Next()
                    {
                        Pair next = this.TrialMove();
                        if (next.Exceeds(this.radius))
                        {
                            this.dir = (this.dir + 1) % 4;
                            next = this.TrialMove();
                        }

                        return next;
                    }

                    private Pair TrialMove() => this.current + this.Dir;
                }
            }
        }

        protected struct Cell
        {
            private readonly Pair pair;
            private readonly int value;

            public Cell(Pair pair, int value)
            {
                this.pair = pair;
                this.value = value;
            }

            public int Distance => this.pair.Distance;

            public int Value => this.value;
        }

        protected struct Pair : IEquatable<Pair>
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

            public bool Exceeds(int r) => (this.DX > r) || (this.DY > r);

            public bool Equals(Pair other) => (this.x == other.x) && (this.y == other.y);
        }
    }
}