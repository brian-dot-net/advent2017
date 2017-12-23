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

            public int Severity()
            {
                int n = this.layers.Count;
                int sev = 0;
                for (int i = 0; i < n; ++i)
                {
                    sev += this.layers[i].Severity(i);
                }

                return sev;
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

                public int Severity(int time)
                {
                    if (this.range == 0)
                    {
                        return 0;
                    }

                    int cycle = 2 * (this.range - 1);
                    if ((cycle > 0) && (time % cycle != 0))
                    {
                        return 0;
                    }

                    return this.depth * this.range;
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