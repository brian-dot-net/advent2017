namespace Advent.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected sealed class Grid
        {
            private readonly Squares squares;

            private Pair pos;
            private Direction dir;

            public Grid(Input input)
            {
                this.squares = new Squares(input);
                this.pos = this.squares.Center;
            }

            public int Run(bool useAllStates, int n)
            {
                int total = 0;
                for (int i = 0; i < n; ++i)
                {
                    total += this.RunOne(useAllStates);
                }

                return total;
            }

            private static int TurnInc(NodeState state)
            {
                switch (state)
                {
                    case NodeState.Clean: return -1;
                    case NodeState.Infected: return 1;
                    case NodeState.Flagged: return 2;
                    default: return 0;
                }
            }

            private int RunOne(bool useAllStates)
            {
                NodeState previous = this.squares.Toggle(useAllStates, this.pos);
                this.Turn(previous);
                this.Move();

                NodeState expected = useAllStates ? NodeState.Weakened : NodeState.Clean;
                return (previous == expected) ? 1 : 0;
            }

            private void Turn(NodeState state)
            {
                int d = (int)this.dir + TurnInc(state);
                this.dir = (Direction)((d + 4) % 4);
            }

            private void Move() => this.pos += this.Dir;

            private Pair Dir
            {
                get
                {
                    switch (this.dir)
                    {
                        case Direction.Up: return new Pair(0, -1);
                        case Direction.Right: return new Pair(1, 0);
                        case Direction.Down: return new Pair(0, 1);
                        default: return new Pair(-1, 0);
                    }
                }
            }

            private enum NodeState : byte
            {
                Clean = 0,
                Weakened = 1,
                Infected = 2,
                Flagged = 3
            }

            private enum Direction
            {
                Up = 0,
                Right = 1,
                Down = 2,
                Left = 3
            }

            private sealed class Squares
            {
                private readonly Dictionary<Pair, Square> squares;
                private readonly int size;

                public Squares(Input input)
                {
                    this.squares = new Dictionary<Pair, Square>();
                    Square first = new Square(input);
                    this.size = first.Size;
                    this.Get(new Pair(), first);
                }

                public Pair Center => new Pair(this.size / 2, this.size / 2);

                private Pair Region(Pair pos) => pos.Region(this.size);

                public NodeState Toggle(bool useAllStates, Pair pos)
                {
                    Square square = this.Get(pos, null);
                    Pair rel = pos.Relative(this.size);
                    NodeState prev = square[rel];
                    int toggle = useAllStates ? 1 : 2;
                    square[rel] = (NodeState)(((int)prev + toggle) % 4);
                    return prev;
                }

                private Square Get(Pair pos, Square newValue)
                {
                    Square square;
                    Pair r = this.Region(pos);
                    if (!this.squares.TryGetValue(r, out square))
                    {
                        square = newValue ?? new Square(this.size);
                        this.squares.Add(r, square);
                    }

                    return square;
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

                public bool Equals(Pair other) => (this.x == other.x) && (this.y == other.y);

                public override int GetHashCode() => this.x & (this.y << 16);

                public Pair Region(int size)
                {
                    return new Pair(Scale(this.x, size), Scale(this.y, size));
                }

                public Pair Relative(int size) => new Pair(ModP(this.x, size), ModP(y, size));

                public int XY(int size) => this.y * size + this.x;

                public static Pair operator +(Pair a, Pair b) => new Pair(a.x + b.x, a.y + b.y);

                private static int ModP(int z, int size) => Math.Abs(z) % size;

                private static int Scale(int z, int size)
                {
                    if (z < 0)
                    {
                        return ((z + 1) / size) - 1;
                    }

                    return z / size;
                }
            }

            private sealed class Square
            {
                private readonly NodeState[] cells;

                public Square(Input input)
                    : this(input.Lines().First().Length)
                {
                    int i = 0;
                    foreach (Input line in input.Lines())
                    {
                        string row = line.ToString();
                        for (int j = 0; j < row.Length; ++j)
                        {
                            this[new Pair(j, i)] = (row[j] == '#') ? NodeState.Infected : NodeState.Clean;
                        }

                        ++i;
                    }
                }

                public Square(int size)
                {
                    this.Size = size;
                    this.cells = new NodeState[size * size];
                }

                public NodeState this[Pair p]
                {
                    get => this.cells[this.XY(p)];
                    set => this.cells[this.XY(p)] = value;
                }

                public int Size { get; private set; }

                private int XY(Pair p) => p.XY(this.Size);
            }
        }
    }
}