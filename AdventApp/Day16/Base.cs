namespace Advent.Day16
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<string>
    {
        protected sealed class Dance
        {
            private readonly int size;
            private readonly Moves moves;

            public Dance(Input input)
            {
                string first = input.Fields(",", 2).First().ToString();
                if (first[0] != 'C')
                {
                    first = "C16";
                }

                this.size = new Input(first.Substring(1)).Integer();
                this.moves = new Moves(input);
            }

            public string Run(int steps)
            {
                Programs result = this.Init();
                int cycle = 0;
                int remaining = steps;
                while (remaining > 0)
                {
                    this.moves.Run(result);
                    --remaining;
                    ++cycle;
                    if (result.IsSorted())
                    {
                        remaining %= cycle;
                        cycle = 0;
                    }
                }

                return result.ToString();
            }

            private Programs Init() => new Programs(this.size);

            private sealed class Moves
            {
                private readonly Move[] moves;

                public Moves(Input input)
                    : this(input.Fields(",").Select(Move.Parse))
                {
                }

                private Moves(IEnumerable<Move> moves)
                {
                    this.moves = moves.ToArray();
                }

                public void Run(Programs programs)
                {
                    foreach (Move move in this.moves)
                    {
                        move.Run(programs);
                    }
                }

                private struct Move
                {
                    private readonly char opcode;
                    private readonly char x;
                    private readonly char y;

                    private Move(char opcode, char x, char y)
                    {
                        this.opcode = opcode;
                        this.x = x;
                        this.y = y;
                    }

                    public static Move Parse(Input input)
                    {
                        string move = input.ToString();
                        char m = move[0];
                        Input[] fields = new Input(move.Substring(1)).Fields("/");
                        switch (m)
                        {
                            case Opcode.Spin:
                                return Spin(fields);
                            case Opcode.Exchange:
                                return Exchange(fields);
                            case Opcode.Partner:
                                return Partner(fields);
                            default:
                                return new Move();
                        }
                    }

                    public void Run(Programs programs)
                    {
                        switch (this.opcode)
                        {
                            case Opcode.Spin:
                                programs.Spin(this.x);
                                break;
                            case Opcode.Exchange:
                                programs.Exchange(this.x, this.y);
                                break;
                            case Opcode.Partner:
                                programs.Partner(this.x, this.y);
                                break;
                        }
                    }

                    private static Move Spin(Input[] fields) => new Move(Opcode.Spin, (char)fields[0].Integer(), '\0');

                    private static Move Exchange(Input[] fields) => new Move(Opcode.Exchange, (char)fields[0].Integer(), (char)fields[1].Integer());

                    private static Move Partner(Input[] fields) => new Move(Opcode.Partner, fields[0].Character(), fields[1].Character());

                    private static class Opcode
                    {
                        public const char Spin = 's';
                        public const char Exchange = 'x';
                        public const char Partner = 'p';
                    }
                }
            }

            private sealed class Programs
            {
                private readonly char[] programs;

                public Programs(int n)
                {
                    this.programs = Enumerable.Range(0, n).Select(i => (char)('a' + i)).ToArray();
                }

                private Programs(Programs other)
                {
                    this.programs = other.programs.ToArray();
                }

                public bool IsSorted()
                {
                    int n = this.programs.Length;
                    for (int i = 0; i < n; ++i)
                    {
                        if (this.programs[i] != ('a' + i))
                        {
                            return false;
                        }
                    }

                    return true;
                }

                public override string ToString()
                {
                    return new string(this.programs);
                }

                public void Spin(int x)
                {
                    int n = this.programs.Length;
                    char[] spun = new char[n];
                    Array.Copy(this.programs, n - x, spun, 0, x);
                    Array.Copy(this.programs, 0, spun, x, n - x);
                    Array.Copy(spun, this.programs, n);
                }

                public void Partner(char x, char y)
                {
                    this.Exchange(this.Find(x), this.Find(y));
                }

                public void Exchange(int i, int j)
                {
                    char t = this.programs[i];
                    this.programs[i] = this.programs[j];
                    this.programs[j] = t;
                }

                private int Find(char a)
                {
                    int n = this.programs.Length;
                    for (int i = 0; i < n; ++i)
                    {
                        if (programs[i] == a)
                        {
                            return i;
                        }
                    }

                    return -1;
                }
            }
        }
    }
}