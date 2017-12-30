namespace Advent.Day24
{
    using System.Collections.Generic;

    public abstract class Base : DayBase<int>
    {
        protected sealed class Bridge
        {
            private readonly Components components;

            public Bridge(Input input)
            {
                this.components = new Components(input);
            }

            public int Build()
            {
                List<Pair> pairs = this.components.Build();
                pairs.Sort((a, b) => b.Weight.CompareTo(a.Weight));
                return pairs[0].Weight;
            }

            public int BuildLongest()
            {
                List<Pair> pairs = this.components.Build();
                pairs.Sort((a, b) => b.CompareLength(a));
                return pairs[0].Weight;
            }

            private struct Pair
            {
                private readonly int weight;
                private readonly int length;

                public Pair(int weight, int length)
                {
                    this.weight = weight;
                    this.length = length;
                }

                public int Weight => this.weight;

                public int Length => this.length;

                public int CompareLength(Pair other)
                {
                    int c = this.length.CompareTo(other.length);
                    if (c == 0)
                    {
                        c = this.weight.CompareTo(other.Weight);
                    }

                    return c;
                }
            }

            private sealed class Components
            {
                private readonly Dictionary<int, HashSet<Component>> components;

                public Components(Input input)
                {
                    this.components = new Dictionary<int, HashSet<Component>>();
                    foreach (Input line in input.Lines())
                    {
                        this.Add(new Component(line));
                    }
                }

                public List<Pair> Build()
                {
                    List<Pair> pairs = new List<Pair>();
                    this.Connect(pairs, new Pair(0, 0), 0);
                    return pairs;
                }

                private void Connect(List<Pair> pairs, Pair pair, int edge)
                {
                    if (pair.Weight > 0)
                    {
                        pairs.Add(pair);
                    }

                    foreach (Component next in this.Find(edge))
                    {
                        Pair p = new Pair(pair.Weight + next.Weight, pair.Length + 1);
                        if (edge == next.X)
                        {
                            this.Connect(pairs, p, next.Y);
                        }
                        else
                        {
                            this.Connect(pairs, p, next.X);
                        }

                        next.Toggle();
                    }
                }

                private IEnumerable<Component> Find(int edge)
                {
                    HashSet<Component> items;
                    if (this.components.TryGetValue(edge, out items))
                    {
                        foreach (Component item in items)
                        {
                            if (!item.Used)
                            {
                                item.Toggle();
                                yield return item;
                            }
                        }
                    }
                }

                private void Add(Component component)
                {
                    this.AddInner(component.X, component);
                    if (component.X != component.Y)
                    {
                        this.AddInner(component.Y, component);
                    }
                }

                private void AddInner(int k, Component component)
                {
                    HashSet<Component> items;
                    if (!this.components.TryGetValue(k, out items))
                    {
                        items = new HashSet<Component>();
                        this.components.Add(k, items);
                    }

                    items.Add(component);
                }
            }

            private sealed class Component
            {
                private readonly int x;
                private readonly int y;

                private bool used;

                private Component(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }

                public Component(Input input)
                {
                    Input[] fields = input.Fields("/");
                    this.x = fields[0].Integer();
                    this.y = fields[1].Integer();
                }

                public void Toggle()
                {
                    this.used = !this.used;
                }

                public bool Used => this.used;

                public int X => this.x;

                public int Y => this.y;

                public int Weight => this.x + this.y;

                public override int GetHashCode()
                {
                    return x + (y << 16);
                }
            }
        }
    }
}