namespace Advent.Day17
{
    using System.Collections.Generic;

    public abstract class Base : DayBase<int>
    {
        protected sealed class Spinlock
        {
            private readonly int steps;
            private readonly List<int> buffer;

            private int position;

            public Spinlock(Input input)
            {
                this.steps = input.Integer();
                this.buffer = new List<int>();
                this.buffer.Add(0);
            }

            public int Run(int n, int index = -1)
            {
                for (int i = 0; i < n; ++i)
                {
                    this.RunOne(i + 1);
                }

                if (index == -1)
                {
                    index = (this.position + 1) % this.buffer.Count;
                }

                return this.buffer[index];
            }

            private void RunOne(int value)
            {
                this.position = 1 + ((this.position + this.steps) % this.buffer.Count);
                this.buffer.Insert(this.position, value);
            }
        }
    }
}