namespace Advent.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected sealed class Disk
        {
            private readonly Grid grid;

            public Disk(Input input)
            {
                this.grid = new Grid(input);
            }

            public int UsedSquares() => this.grid.UsedSquares();

            public int Regions()
            {
                HashSet<Pair> visited = new HashSet<Pair>();
                Pair current = P(0, 0);
                int total = 0;
                do
                {
                    if (this.IsRegion(visited, current))
                    {
                        ++total;
                    }

                    current = Next(current);
                }
                while (!current.Equals(P(0, 0)));

                return total;
            }

            private static Pair Next(Pair p)
            {
                Pair q = p + P(1, 0);
                if (IsValid(q))
                {
                    return q;
                }

                q = P(0, p.Y + 1);
                if (!IsValid(q))
                {
                    q = P(0, 0);
                }

                return q;
            }

            private static bool IsValid(Pair p) => (p.X >= 0) && (p.X < 128) && (p.Y >= 0) && (p.Y < 128);

            private static Pair P(int x, int y) => new Pair(x, y);

            private static void EnqueueIfValid(Queue<Pair> next, Pair p)
            {
                if (IsValid(p))
                {
                    next.Enqueue(p);
                }
            }

            private static void EnqueueAdjacent(Queue<Pair> next, Pair current)
            {
                EnqueueIfValid(next, current + P(1, 0));
                EnqueueIfValid(next, current + P(-1, 0));
                EnqueueIfValid(next, current + P(0, -1));
                EnqueueIfValid(next, current + P(0, 1));
            }

            private bool IsRegion(HashSet<Pair> visited, Pair start)
            {
                int size = 0;
                Queue<Pair> next = new Queue<Pair>();
                next.Enqueue(start);
                while (next.Count > 0)
                {
                    Pair current = next.Dequeue();
                    if (visited.Add(current) && this.grid.IsUsed(current))
                    {
                        ++size;
                        EnqueueAdjacent(next, current);
                    }
                }

                return size > 0;
            }

            private sealed class Grid
            {
                private readonly GridRow[] rows;

                public Grid(Input input)
                {
                    this.rows = new GridRow[128];
                    Knot knot = new Knot(256);

                    for (int i = 0; i < this.rows.Length; ++i)
                    {
                        this.rows[i] = new GridRow(new Input(input + "-" + i));
                    }
                }

                public int UsedSquares() => this.rows.Select(r => r.UsedSquares()).Sum();

                public bool IsUsed(Pair p) => this.rows[p.Y].IsUsed(p.X);

                private struct GridRow
                {
                    private static readonly OneBitsTable Table = new OneBitsTable();

                    private readonly byte[] row;

                    public GridRow(Input input)
                    {
                        this.row = Knot.Hash(input);
                    }

                    public int UsedSquares() => this.row.Select(b => Table[b]).Sum();

                    public bool IsUsed(int x)
                    {
                        byte b = this.row[x / 8];
                        int s = 7 - (x % 8);
                        return ((b >> s) & 1) == 1;
                    }
                }
            }

            private struct Pair : IEquatable<Pair>
            {
                private readonly int x;
                private readonly int y;

                public Pair(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }

                public int X => this.x;

                public int Y => this.y;

                public static Pair operator +(Pair a, Pair b) => new Pair(a.x + b.x, a.y + b.y);

                public override int GetHashCode() => (this.y << 16) & this.x;

                public bool Equals(Pair other) => (this.x == other.x) && (this.y == other.y);
            }
        }
    }
}