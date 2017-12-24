namespace Advent
{
    using System;
    using System.Linq;

    public abstract class Day16 : DayBase<string>
    {
        protected sealed class Dance
        {
            private readonly Programs programs;

            public Dance(Input input)
            {
                string first = input.Fields(",", 2).First().ToString();
                if (first[0] != 'C')
                {
                    first = "C16";
                }

                int n = new Input(first.Substring(1)).Integer();
                this.programs = new Programs(n);
            }

            public string Run(Input input)
            {
                Moves moves = new Moves(input);
                moves.Run(this.programs);
                return this.programs.ToString();
            }

            private sealed class Moves
            {
                private readonly Move[] moves;

                public Moves(Input input)
                {
                    this.moves = input.Fields(",").Select(Move.Parse).ToArray();
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