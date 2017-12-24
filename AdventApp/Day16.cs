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
                Moves moves = new Moves();
                moves.Run(input, this.programs);
                return this.programs.ToString();
            }

            private sealed class Moves
            {
                public void Run(Input input, Programs programs)
                {
                    foreach (Input move in input.Fields(","))
                    {
                        Move(move.ToString(), programs);
                    }
                }

                private static void Move(string move, Programs programs)
                {
                    char m = move[0];
                    Input rest = new Input(move.Substring(1));
                    switch (m)
                    {
                        case 's':
                            programs.Spin(rest);
                            break;
                        case 'x':
                            programs.Exchange(rest);
                            break;
                        case 'p':
                            programs.Partner(rest);
                            break;
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

                public void Spin(Input input)
                {
                    Input[] fields = input.Fields("/");
                    int x = fields[0].Integer();
                    this.Spin(x);
                }

                public void Exchange(Input input)
                {
                    Input[] fields = input.Fields("/");
                    int x = fields[0].Integer();
                    int y = fields[1].Integer();
                    this.Exchange(x, y);
                }

                public void Partner(Input input)
                {
                    Input[] fields = input.Fields("/");
                    char x = fields[0].Character();
                    char y = fields[1].Character();
                    this.Partner(x, y);
                }

                private void Spin(int x)
                {
                    int n = this.programs.Length;
                    char[] spun = new char[n];
                    Array.Copy(this.programs, n - x, spun, 0, x);
                    Array.Copy(this.programs, 0, spun, x, n - x);
                    Array.Copy(spun, this.programs, n);
                }

                private void Partner(char x, char y)
                {
                    this.Exchange(this.Find(x), this.Find(y));
                }

                private void Exchange(int i, int j)
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