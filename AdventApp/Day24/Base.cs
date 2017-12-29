using System;
using System.Collections.Generic;

namespace Advent.Day24
{
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
                List<int> weights = this.components.Build();
                weights.Sort();
                return weights[weights.Count - 1];
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

                public List<int> Build()
                {
                    List<int> weights = new List<int>();
                    this.Connect(weights, 0, 0);
                    return weights;
                }

                private void Connect(List<int> weights, int weight, int edge)
                {
                    if (weight > 0)
                    {
                        weights.Add(weight);
                    }

                    foreach (Component next in this.Find(edge))
                    {
                        int w = weight + next.Weight;
                        if (edge == next.X)
                        {
                            this.Connect(weights, w, next.Y);
                        }
                        else
                        {
                            this.Connect(weights, w, next.X);
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