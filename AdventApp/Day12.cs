namespace Advent
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Day12 : DayBase<int>
    {
        protected sealed class Pipes
        {
            private readonly Dictionary<int, Pipe> groups;

            public Pipes(Input input)
            {
                this.groups = new Dictionary<int, Pipe>();
                foreach (Input[] fields in input.Lines().Select(l => l.Fields(" <-> ")))
                {
                    int program = fields[0].Integer();
                    IEnumerable<int> connections = fields[1].Fields(", ").Select(f => f.Integer());
                    this.Connect(program, connections);
                }
            }

            public int Count(int program)
            {
                return this.groups[program].Count();
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
                Pipe pipe;
                if (!this.groups.TryGetValue(program, out pipe))
                {
                    pipe = new Pipe(program);
                    this.groups.Add(program, pipe);
                }

                return pipe;
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

                public int Count()
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

                    return visited.Count;
                }
            }
        }
    }
}