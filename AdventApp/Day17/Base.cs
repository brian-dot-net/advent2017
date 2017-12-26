namespace Advent.Day17
{
    using System.Collections.Generic;

    public abstract class Base : DayBase<int>
    {
        protected sealed class Spinlock
        {
            private readonly int steps;
            private readonly List<int> buffer;

            private int count;
            private int position;

            public Spinlock(Input input)
            {
                this.steps = input.Integer();
                this.buffer = new List<int>();
                this.Insert(0);
            }

            public int Run(int n, int index = -1)
            {
                if (index == -1)
                {
                    return this.RunReal(n);
                }

                return this.RunShortcut(n, index);
            }

            private int RunShortcut(int n, int index)
            {
                int value = 0;
                for (int i = 0; i < n; ++i)
                {
                    if (this.RunOne(i + 1, index))
                    {
                        value = i + 1;
                    }
                }

                return value;
            }

            private int RunReal(int n)
            {
                for (int i = 0; i < n; ++i)
                {
                    this.RunOne(i + 1);
                }

                return this.buffer[(this.position + 1) % this.buffer.Count];
            }

            private bool RunOne(int value, int index = -1)
            {
                this.position = 1 + ((this.position + this.steps) % this.count);
                return this.Insert(value, index);
            }

            private bool Insert(int value, int index = -1)
            {
                if (index == -1)
                {
                    this.buffer.Insert(this.position, value);
                }

                ++this.count;
                return this.position == index;
            }
        }
    }
}