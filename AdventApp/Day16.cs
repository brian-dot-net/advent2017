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
                foreach (Input move in input.Fields(","))
                {
                    this.Move(move.ToString());
                }

                return this.programs.ToString();
            }

            private void Move(string move)
            {
                char m = move[0];
                Input rest = new Input(move.Substring(1));
                switch (m)
                {
                    case 's':
                        this.programs.Spin(rest);
                        break;
                    case 'x':
                        this.programs.Exchange(rest);
                        break;
                    case 'p':
                        this.programs.Partner(rest);
                        break;
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
                    char[] spun = this.programs.ToArray();
                    int s = input.Integer();
                    int n = this.programs.Length;
                    Array.Copy(this.programs, n - s, spun, 0, s);
                    Array.Copy(this.programs, 0, spun, s, n - s);
                    Array.Copy(spun, this.programs, n);
                }

                public void Exchange(Input input)
                {
                    Input[] fields = input.Fields("/");
                    int i = fields[0].Integer();
                    int j = fields[1].Integer();
                    this.Swap(i, j);
                }

                public void Partner(Input input)
                {
                    Input[] fields = input.Fields("/");
                    char a = fields[0].Character();
                    char b = fields[1].Character();
                    this.Swap(this.Find(a), this.Find(b));
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

                private void Swap(int i, int j)
                {
                    char t = this.programs[i];
                    this.programs[i] = this.programs[j];
                    this.programs[j] = t;
                }
            }
        }
    }
}