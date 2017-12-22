namespace Advent
{
    using System.Collections.Generic;

    public abstract class Day09 : DayBase<int>
    {
        public override string DefaultInput => null;

        protected sealed class Groups
        {
            private readonly Group root;

            public Groups(string input)
            {
                this.root = Group.Parse(input);
            }

            public int Score() => this.root.Score();

            public int CountGarbage() => this.root.CountGarbage();

            private sealed class Group
            {
                private readonly List<Group> children;
                private readonly bool garbage;

                private int garbageCount;

                private Group(Group parent, bool garbage)
                {
                    this.children = new List<Group>();
                    this.garbage = garbage;
                    if (parent != null)
                    {
                        parent.Add(this);
                    }
                }

                public static Group Parse(string input)
                {
                    return new Parser(input).Run();
                }

                public int Score()
                {
                    return this.ScoreInner(0);
                }

                public void OnGarbageChar()
                {
                    ++this.garbageCount;
                }

                public int CountGarbage()
                {
                    int total = this.garbageCount;
                    if (total > 0)
                    {
                        return total;
                    }

                    foreach (Group child in this.children)
                    {
                        total += child.CountGarbage();
                    }

                    return total;
                }

                private void Add(Group child) => this.children.Add(child);

                private int ScoreInner(int depth)
                {
                    int total = 0;
                    if (!this.garbage)
                    {
                        total = 1 + depth;
                    }

                    foreach (Group child in this.children)
                    {
                        total += child.ScoreInner(1 + depth);
                    }

                    return total;
                }

                private sealed class Parser
                {
                    private readonly Stack<Group> stack;
                    private readonly string input;

                    public Parser(string input)
                    {
                        this.stack = new Stack<Group>();
                        this.input = input;
                    }

                    public Group Run()
                    {
                        int n = this.input.Length;
                        Group root = null;
                        Group garbage = null;
                        for (int i = 0; i < n; ++i)
                        {
                            char c = this.input[i];
                            if (garbage != null)
                            {
                                if (c == '>')
                                {
                                    garbage = null;
                                    this.Pop();
                                }
                                else if (c == '!')
                                {
                                    ++i;
                                }
                                else
                                {
                                    garbage.OnGarbageChar();
                                }
                            }
                            else if (c == '<')
                            {
                                garbage = this.Push(true);
                            }
                            else if (c == '{')
                            {
                                this.Push(false);
                            }
                            else if (c == '}')
                            {
                                root = this.Pop();
                            }
                        }

                        return root;
                    }

                    private Group Push(bool garbage)
                    {
                        Group parent = null;
                        if (this.stack.Count > 0)
                        {
                            parent = this.stack.Peek();
                        }

                        Group group = new Group(parent, garbage);
                        this.stack.Push(group);
                        return group;
                    }

                    private Group Pop() => this.stack.Pop();
                }
            }
        }
    }
}