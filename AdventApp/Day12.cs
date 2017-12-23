namespace Advent
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Day12 : DayBase<int>
    {
        protected sealed class Pipes
        {
            private readonly List<Pipe> programs;

            public Pipes(Input input)
            {
                this.programs = new List<Pipe>();
                foreach (Input[] fields in input.Lines().Select(l => l.Fields(" <-> ")))
                {
                    int program = fields[0].Integer();
                    IEnumerable<int> connections = fields[1].Fields(", ").Select(f => f.Integer());
                    this.Connect(program, connections);
                }
            }

            private int ProgramCount => this.programs.Count;

            public int CountPipes(int program)
            {
                return this.Connected(program).Count;
            }

            public int CountGroups()
            {
                return new Groups(this).Count;
            }

            private HashSet<int> Connected(int program)
            {
                return this.Get(program).Connected();
            }

            private void Connect(int program, IEnumerable<int> connections)
            {
                Pipe pipe = this.Get(program);
                foreach (int connection in connections)
                {
                    pipe.Connect(this.Get(connection));
                }
            }

            private Pipe Get(int program)
            {
                for (int i = this.programs.Count; i <= program; ++i)
                {
                    this.programs.Add(new Pipe(i));
                }

                return this.programs[program];
            }

            private sealed class Groups
            {
                private readonly Pipes pipes;
                private readonly HashSet<int> all;

                public Groups(Pipes pipes)
                {
                    this.pipes = pipes;
                    this.all = new HashSet<int>();
                    this.Init();
                }

                public int Count { get; private set; }

                private void Init()
                {
                    int i = 0;
                    int n = this.pipes.ProgramCount;
                    while (this.all.Count < n)
                    {
                        if (this.AddGroup(i))
                        {
                            ++this.Count;
                        }

                        ++i;
                    }
                }

                private bool AddGroup(int i)
                {
                    HashSet<int> group = this.pipes.Connected(i);
                    foreach (int p in group)
                    {
                        if (!this.all.Add(p))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            private sealed class Pipe
            {
                private readonly List<Pipe> connections;
                private readonly int program;

                public Pipe(int program)
                {
                    this.connections = new List<Pipe>();
                    this.program = program;
                }

                public void Connect(Pipe other)
                {
                    this.connections.Add(other);
                }

                public HashSet<int> Connected()
                {
                    HashSet<int> visited = new HashSet<int>();
                    Queue<Pipe> next = new Queue<Pipe>();
                    next.Enqueue(this);
                    while (next.Count > 0)
                    {
                        Pipe current = next.Dequeue();
                        visited.Add(current.program);

                        foreach (Pipe connection in current.connections)
                        {
                            if (!visited.Contains(connection.program))
                            {
                                next.Enqueue(connection);
                            }
                        }
                    }

                    return visited;
                }
            }
        }
    }
}