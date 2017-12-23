namespace Advent
{
    using System.Collections.Generic;

    public abstract class Day13 : DayBase<int>
    {
        protected sealed class Firewall
        {
            private readonly List<Layer> layers;

            public Firewall(Input input)
            {
                this.layers = new List<Layer>();
                foreach (Input line in input.Lines())
                {
                    this.Add(Layer.Parse(line));
                }
            }

            public int SendPacket()
            {
                int n = this.layers.Count;
                int sev = 0;
                for (int i = 0; i < n; ++i)
                {
                    sev += this.layers[i].Send(i);
                }

                return sev;
            }

            public bool TryPacket(int delay)
            {
                int n = this.layers.Count;
                for (int i = 0; i < n; ++i)
                {
                    if (!this.layers[i].Try(i + delay))
                    {
                        return false;
                    }
                }

                return true;
            }

            private void Add(Layer layer)
            {
                for (int i = this.layers.Count; i < layer.Depth; ++i)
                {
                    this.layers.Add(new Layer(i, 0));
                }

                this.layers.Add(layer);
            }

            private struct Layer
            {
                private readonly int depth;
                private readonly int range;

                public Layer(int depth, int range)
                {
                    this.depth = depth;
                    this.range = range;
                }

                public int Depth => this.depth;

                public int Send(int time)
                {
                    if (this.Try(time))
                    {
                        return 0;
                    }

                    return this.depth * this.range;
                }

                public bool Try(int time)
                {
                    if (this.range == 0)
                    {
                        return true;
                    }

                    int cycle = 2 * (this.range - 1);
                    if ((cycle > 0) && (time % cycle != 0))
                    {
                        return true;
                    }

                    return false;
                }

                public static Layer Parse(Input line)
                {
                    Input[] row = line.Fields(": ");
                    return new Layer(row[0].Integer(), row[1].Integer());
                }
            }
        }
    }
}