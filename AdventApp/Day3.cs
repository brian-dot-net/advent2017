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


        private static Pair P(int x, int y) => new Pair(x, y);

        protected sealed class Spiral
        {
            public Spiral()
            {
            }

            public IEnumerable<Cell> Cells(bool useSums)
            {
                return this.Rings(useSums).SelectMany(r => r.Cells());
            }

            private IEnumerable<Ring> Rings(bool useSums)
            {
                Ring current = Ring.First(useSums);
                while (true)
                {
                    yield return current;
                    current = current.Next();
                }
            }

            private sealed class Ring
            {
                private readonly CellCollection values;
                private readonly int radius;

                private Ring(CellCollection values, int radius)
                {
                    this.values = values;
                    this.radius = radius;
                }

                private int Count => 8 * this.radius;

                public static Ring First(bool useSums)
                {
                    return new Ring(CellCollection.New(useSums), 0);
                }

                public Ring Next()
                {
                    return new Ring(this.values, this.radius + 1);
                }

                public IEnumerable<Cell> Cells()
                {
                    return new RingCells(values, this.radius).All();
                }

                private abstract class CellCollection
                {
                    private readonly Dictionary<Pair, int> values;

                    protected CellCollection()
                    {
                        this.values = new Dictionary<Pair, int>();
                    }

                    public static CellCollection New(bool useSums)
                    {
                        return new SequentialCellCollection();
                    }

                    public Cell Get(Pair pair)
                    {
                        int value;
                        if (!this.values.TryGetValue(pair, out value))
                        {
                            value = this.GetValue(pair);
                            this.values.Add(pair, value);
                        }

                        return new Cell(pair, value);
                    }

                    protected abstract int GetValue(Pair pair);

                    private sealed class SequentialCellCollection : CellCollection
                    {
                        private int value;

                        protected override int GetValue(Pair pair)
                        {
                            return ++this.value;
                        }
                    }
                }

                private sealed class RingCells
                {
                    private readonly CellCollection values;
                    private readonly int radius;

                    private Pair current;
                    private int dir;

                    public RingCells(CellCollection values, int radius)
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

                    private Pair TrialMove()
                    {
                        return this.current + this.Dir;
                    }
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

        protected struct Pair
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

            public override string ToString()
            {
                return "(" + this.x + ", " + this.y + ")";
            }
        }
    }
}