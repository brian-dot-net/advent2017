namespace Advent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Day07 : DayBase<string>
    {
        public override string DefaultInput => null;

        protected sealed class ProgramNode
        {
            private readonly List<ProgramNode> children;

            public ProgramNode(string name)
            {
                this.Name = name;
                this.children = new List<ProgramNode>();
            }

            public string Name { get; private set; }

            public int Weight { get; set; }

            public void AddChild(ProgramNode child) => this.children.Add(child);

            public string GetImbalance()
            {
                Tuple<int, ProgramNode, int> current = Tuple.Create(0, this, 0);
                while (true)
                {
                    Tuple<int, ProgramNode, int> next = current.Item2.GetImbalanceInner();
                    if (next.Item2 == null)
                    {
                        ProgramNode badNode = current.Item2;
                        int requiredWeight = badNode.Weight + current.Item1 - current.Item3;
                        return badNode.Name + "=" + requiredWeight;
                    }

                    current = next;
                }
            }

            private int TotalWeight()
            {
                return this.children.Select(c => c.TotalWeight()).Sum() + this.Weight;
            }

            private Tuple<int, ProgramNode, int> GetImbalanceInner()
            {
                ProgramNode badNode = null;
                if (this.children.Count < 3)
                {
                    return Tuple.Create(0, badNode, 0);
                }

                int[] weights = new int[3];
                for (int i = 0; i < 3; ++i)
                {
                    weights[i] = this.children[i].TotalWeight();
                }

                int bad = -1;
                int good = 0;
                if (weights[0] != weights[1])
                {
                    bad = (weights[0] == weights[2]) ? 1 : 0;
                    good = bad + 1;
                }
                else if (weights[0] != weights[2])
                {
                    bad = 2;
                }

                int goodWeight = weights[good];
                int badWeight;
                if (bad == -1)
                {
                    badWeight = 0;
                    for (int i = 3; i < this.children.Count; ++i)
                    {
                        int weight = this.children[i].TotalWeight();
                        if (goodWeight != weight)
                        {
                            bad = i;
                            badWeight = weight;
                            break;
                        }
                    }
                }
                else
                {
                    badWeight = weights[bad];
                }

                if (bad != -1)
                {
                    badNode = this.children[bad];
                }

                return Tuple.Create(goodWeight, badNode, badWeight);
            }
        }

        protected sealed class ProgramTree
        {
            public ProgramTree(string input)
            {
                this.Root = new RawProgramTree(input).FindRoot();
            }

            public ProgramNode Root { get; private set; }

            private sealed class RawProgramTree
            {
                private readonly Dictionary<string, ProgramNode> nodes;
                private readonly HashSet<string> parentNodes;
                private readonly HashSet<string> childNodes;

                public RawProgramTree(string input)
                {
                    this.nodes = new Dictionary<string, ProgramNode>();
                    this.parentNodes = new HashSet<string>();
                    this.childNodes = new HashSet<string>();

                    foreach (ProgramInfo info in Lines.From(input).Select(Parse))
                    {
                        int childCount = this.Add(info);
                        if (childCount > 0)
                        {
                            this.parentNodes.Add(info.Name);
                        }
                    }
                }

                public ProgramNode FindRoot()
                {
                    this.parentNodes.ExceptWith(this.childNodes);
                    return this.Get(this.parentNodes.Single());
                }

                private int Add(ProgramInfo info)
                {
                    ProgramNode node = this.Get(info.Name);
                    node.Weight = int.Parse(info.Weight.Trim('(', ')'));
                    int childCount = 0;
                    foreach (string child in info.Children)
                    {
                        this.AddChild(node, child);
                        ++childCount;
                    }

                    return childCount;
                }

                private void AddChild(ProgramNode node, string child)
                {
                    node.AddChild(this.Get(child));
                    this.childNodes.Add(child);
                }

                private ProgramNode Get(string name)
                {
                    ProgramNode node;
                    if (!this.nodes.TryGetValue(name, out node))
                    {
                        node = new ProgramNode(name);
                        this.nodes.Add(name, node);
                    }

                    return node;
                }

                private static ProgramInfo Parse(string line)
                {
                    string[] fields = line.Split(new char[] { ' ' }, 4);
                    ProgramInfo info = new ProgramInfo
                    {
                        Name = fields[0],
                        Weight = fields[1]
                    };

                    if (fields.Length == 4)
                    {
                        foreach (string child in fields[3].Split(new char[] { ',' }))
                        {
                            info.Children.Add(child.Trim());
                        }
                    }

                    return info;
                }

                private sealed class ProgramInfo
                {
                    public ProgramInfo()
                    {
                        this.Children = new List<string>();
                    }

                    public string Name { get; set; }

                    public string Weight { get; set; }

                    public IList<string> Children { get; private set; }
                }
            }
        }
    }
}