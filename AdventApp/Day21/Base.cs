namespace Advent.Day21
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected sealed class Art
        {
            private readonly Rules rules;
            private readonly int iterations;

            public Art(Input input)
            {
                this.rules = new Rules();
                foreach (Input line in input.Lines())
                {
                    Input[] fields = line.Fields(" => ");
                    if (fields[0].ToString() == "N")
                    {
                        this.iterations = fields[1].Integer();
                    }
                    else
                    {
                        this.rules.Add(fields[0], fields[1]);
                    }
                }
            }

            public int Run(int n)
            {
                if (this.iterations != 0)
                {
                    n = this.iterations;
                }

                Grid grid = new Grid(new Input(".#./..#/###"));
                for (int i = 0; i < n; ++i)
                {
                    grid = grid.Transform(this.rules);
                }

                return grid.CountOn();
            }

            private static int B(bool b, int s) => (b ? 1 : 0) << s;

            private static char C(bool b) => b ? '#' : '.';

            private sealed class Grid
            {
                private readonly bool[][] grid;

                public Grid(Input input)
                {
                    this.grid = input.Fields("/").Select(ParseBools).ToArray();
                }

                private Grid(bool[][] grid)
                {
                    this.grid = grid;
                }

                public bool this[int y, int x]
                {
                    get => this.grid[y][x];
                    set => this.grid[y][x] = value;
                }

                public int Size => this.grid.Length;

                public int CountOn() => this.grid.Select(CountTrue).Sum();

                public Grid Transform(Rules rules)
                {
                    if ((this.Size % 2) == 0)
                    {
                        return this.Transform2(rules);
                    }

                    return this.Transform3(rules);
                }

                private static int CountTrue(bool[] row) => row.Select(b => b ? 1 : 0).Sum();

                private static bool[] ParseBools(Input input) => input.ToString().Select(c => c == '#').ToArray();

                private static Grid New<TPattern>(TPattern[][] input) where TPattern : IPattern
                {
                    int h = input.Length;
                    int s = input[0][0].Size;
                    bool[][] g = new bool[h * s][];
                    Grid newGrid = new Grid(g);
                    for (int i = 0; i < h; ++i)
                    {
                        int w = input[i].Length;
                        for (int k = 0; k < s; ++k)
                        {
                            g[s * i + k] = new bool[w * s];
                        }

                        for (int j = 0; j < w; ++j)
                        {
                            input[i][j].CopyTo(newGrid, i * s, j * s);
                        }
                    }

                    return newGrid;
                }

                private Grid Transform2(Rules rules)
                {
                    const int D = 2;
                    int s = this.Size / D;
                    Pattern3[][] output = new Pattern3[s][];
                    for (int i = 0; i < s; ++i)
                    {
                        output[i] = new Pattern3[s];
                        int dy = D * i;
                        for (int j = 0; j < s; ++j)
                        {
                            int dx = D * j;
                            Pattern2 input = new Pattern2(this, dy, dx);
                            Pattern3 outputTile = rules.Get(input);
                            output[i][j] = outputTile;
                        }
                    }

                    return Grid.New(output);
                }

                private Grid Transform3(Rules rules)
                {
                    const int D = 3;
                    int s = this.Size / D;
                    Pattern4[][] output = new Pattern4[s][];
                    for (int i = 0; i < s; ++i)
                    {
                        output[i] = new Pattern4[s];
                        int dy = D * i;
                        for (int j = 0; j < s; ++j)
                        {
                            int dx = D * j;
                            Pattern3 input = new Pattern3(this, dy, dx);
                            Pattern4 outputTile = rules.Get(input);
                            output[i][j] = outputTile;
                        }
                    }

                    return Grid.New(output);
                }
            }

            private interface IPattern
            {
                int Size { get; }

                void CopyTo(Grid output, int dy, int dx);
            }

            private struct Pattern2 : IEquatable<Pattern2>, IPattern
            {
                private readonly bool a;
                private readonly bool b;
                private readonly bool c;
                private readonly bool d;

                public Pattern2(Grid input)
                    : this(input, 0, 0)
                {
                }

                public Pattern2(Grid input, int dy, int dx)
                {
                    this.a = input[dy, dx];
                    this.b = input[dy, dx + 1];
                    this.c = input[dy + 1, dx];
                    this.d = input[dy + 1, dx + 1];
                }

                private Pattern2(bool a, bool b, bool c, bool d)
                {
                    this.a = a;
                    this.b = b;
                    this.c = c;
                    this.d = d;
                }

                public int Size => 2;

                public void CopyTo(Grid output, int dy, int dx)
                {
                    output[dy, dx] = this.a;
                    output[dy, dx + 1] = this.b;
                    output[dy + 1, dx] = this.c;
                    output[dy + 1, dx + 1] = this.d;
                }

                public bool Equals(Pattern2 other)
                {
                    return
                        (this.a == other.a) &&
                        (this.b == other.b) &&
                        (this.c == other.c) &&
                        (this.d == other.d);
                }

                public override int GetHashCode()
                {
                    return
                        B(this.a, 0) |
                        B(this.b, 1) |
                        B(this.c, 2) |
                        B(this.d, 3);
                }

                public void EachRotation(Action<Pattern2> each)
                {
                    this.EachFlip(each);
                    new Pattern2(this.c, this.a, this.d, this.b).EachFlip(each);
                    new Pattern2(this.d, this.c, this.b, this.a).EachFlip(each);
                    new Pattern2(this.b, this.d, this.a, this.c).EachFlip(each);
                }

                private void EachFlip(Action<Pattern2> each)
                {
                    each(this);
                    each(new Pattern2(this.b, this.a, this.d, this.c));
                }
            }

            private struct Pattern3 : IEquatable<Pattern3>, IPattern
            {
                private readonly bool a;
                private readonly bool b;
                private readonly bool c;
                private readonly bool d;
                private readonly bool e;
                private readonly bool f;
                private readonly bool g;
                private readonly bool h;
                private readonly bool i;

                public Pattern3(Grid input)
                    : this(input, 0, 0)
                {
                }

                public Pattern3(Grid input, int dy, int dx)
                {
                    this.a = input[dy, dx];
                    this.b = input[dy, dx + 1];
                    this.c = input[dy, dx + 2];
                    this.d = input[dy + 1, dx];
                    this.e = input[dy + 1, dx + 1];
                    this.f = input[dy + 1, dx + 2];
                    this.g = input[dy + 2, dx];
                    this.h = input[dy + 2, dx + 1];
                    this.i = input[dy + 2, dx + 2];
                }

                private Pattern3(bool a, bool b, bool c, bool d, bool e, bool f, bool g, bool h, bool i)
                {
                    this.a = a;
                    this.b = b;
                    this.c = c;
                    this.d = d;
                    this.e = e;
                    this.f = f;
                    this.g = g;
                    this.h = h;
                    this.i = i;
                }

                public int Size => 3;

                public void CopyTo(Grid output, int dy, int dx)
                {
                    output[dy, dx] = this.a;
                    output[dy, dx + 1] = this.b;
                    output[dy, dx + 2] = this.c;
                    output[dy + 1, dx] = this.d;
                    output[dy + 1, dx + 1] = this.e;
                    output[dy + 1, dx + 2] = this.f;
                    output[dy + 2, dx] = this.g;
                    output[dy + 2, dx + 1] = this.h;
                    output[dy + 2, dx + 2] = this.i;
                }

                public bool Equals(Pattern3 other)
                {
                    return
                        (this.a == other.a) &&
                        (this.b == other.b) &&
                        (this.c == other.c) &&
                        (this.d == other.d) &&
                        (this.e == other.e) &&
                        (this.f == other.f) &&
                        (this.g == other.g) &&
                        (this.h == other.h) &&
                        (this.i == other.i);
                }

                public override int GetHashCode()
                {
                    return
                        B(this.a, 0) |
                        B(this.b, 1) |
                        B(this.c, 2) |
                        B(this.d, 3) |
                        B(this.e, 4) |
                        B(this.f, 5) |
                        B(this.g, 6) |
                        B(this.h, 7) |
                        B(this.i, 8);
                }

                public void EachRotation(Action<Pattern3> each)
                {
                    this.EachFlip(each);
                    new Pattern3(this.g, this.d, this.a, this.h, this.e, this.b, this.i, this.f, this.c).EachFlip(each);
                    new Pattern3(this.i, this.h, this.g, this.f, this.e, this.d, this.c, this.b, this.a).EachFlip(each);
                    new Pattern3(this.c, this.f, this.i, this.b, this.e, this.h, this.a, this.d, this.g).EachFlip(each);
                }

                private void EachFlip(Action<Pattern3> each)
                {
                    each(this);
                    each(new Pattern3(this.g, this.h, this.i, this.d, this.e, this.f, this.a, this.b, this.c));
                }
            }

            private struct Pattern4 : IPattern
            {
                private readonly Pattern2 pa;
                private readonly Pattern2 pb;
                private readonly Pattern2 pc;
                private readonly Pattern2 pd;

                public Pattern4(Grid input)
                {
                    this.pa = new Pattern2(input, 0, 0);
                    this.pb = new Pattern2(input, 0, 2);
                    this.pc = new Pattern2(input, 2, 0);
                    this.pd = new Pattern2(input, 2, 2);
                }

                public int Size => 4;

                public void CopyTo(Grid output, int dy, int dx)
                {
                    this.pa.CopyTo(output, dy, dx);
                    this.pb.CopyTo(output, dy, dx + 2);
                    this.pc.CopyTo(output, dy + 2, dx);
                    this.pd.CopyTo(output, dy + 2, dx + 2);
                }
            }

            private sealed class Rules
            {
                private readonly Dictionary<Pattern2, Pattern3> patterns2;
                private readonly Dictionary<Pattern3, Pattern4> patterns3;

                public Rules()
                {
                    this.patterns2 = new Dictionary<Pattern2, Pattern3>();
                    this.patterns3 = new Dictionary<Pattern3, Pattern4>();
                }

                public Pattern3 Get(Pattern2 input) => this.patterns2[input];

                public Pattern4 Get(Pattern3 input) => this.patterns3[input];

                public void Add(Input input, Input output)
                {
                    Grid inputGrid = new Grid(input);
                    Grid outputGrid = new Grid(output);
                    if (inputGrid.Size == 2)
                    {
                        this.Add2(inputGrid, outputGrid);
                    }
                    else
                    {
                        this.Add3(inputGrid, outputGrid);
                    }
                }

                private void Add2(Grid input2, Grid output3)
                {
                    Pattern2 input = new Pattern2(input2);
                    Pattern3 output = new Pattern3(output3);
                    input.EachRotation(r => this.patterns2[r] = output);
                }

                private void Add3(Grid input3, Grid output4)
                {
                    Pattern3 input = new Pattern3(input3);
                    Pattern4 output = new Pattern4(output4);
                    input.EachRotation(r => this.patterns3[r] = output);
                }
            }
        }
    }
}