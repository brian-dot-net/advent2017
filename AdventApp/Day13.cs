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
                    this.layers.Add(Layer.Parse(line));
                }
            }

            public int SendPacket()
            {
                int sev = 0;
                foreach (Layer layer in this.layers)
                {
                    sev += layer.Send();
                }

                return sev;
            }

            public bool TryPacket(int delay)
            {
                foreach (Layer layer in this.layers)
                {
                    if (!layer.Try(delay))
                    {
                        return false;
                    }
                }

                return true;
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

                public int Send()
                {
                    if (this.Try(0))
                    {
                        return 0;
                    }

                    return this.depth * this.range;
                }

                public bool Try(int delay)
                {
                    if (this.range == 0)
                    {
                        return true;
                    }

                    int time = this.depth + delay;
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