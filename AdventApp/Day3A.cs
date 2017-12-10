namespace Advent
{
    using System;

    public sealed class Day3A : Day3
    {
        protected override int RunCore(string input)
        {
            int n = int.Parse(input);
            Steps steps = new Steps(n);
            while (steps.MoveNext())
            {
            }

            return steps.Distance;
        }

        private sealed class Steps
        {
            private int remaining;
            private int stride;
            private Direction dir;
            private int x;
            private int y;
            private int step;

            public Steps(int n)
            {
                this.remaining = n - 1;
            }

            public int Distance
            {
                get => Math.Abs(this.x) + Math.Abs(this.y);
            }

            public bool MoveNext()
            {
                this.Turn();
                bool hasMore = this.remaining > this.stride;
                if (!hasMore)
                {
                    this.stride = this.remaining;
                }

                this.MoveNow();
                this.remaining -= this.stride;

                return hasMore;
            }

            private void Turn()
            {
                this.dir = (Direction)(((int)this.dir + 1) % 4);
                if (this.step % 2 == 0)
                {
                    ++this.stride;
                }

                ++this.step;
            }

            private void MoveNow()
            {
                switch (this.dir)
                {
                    case Direction.Down:
                        this.y -= this.stride;
                        break;
                    case Direction.Right:
                        this.x += this.stride;
                        break;
                    case Direction.Up:
                        this.y += this.stride;
                        break;
                    case Direction.Left:
                        this.x -= this.stride;
                        break;
                }
            }

            private enum Direction
            {
                Down,
                Right,
                Up,
                Left
            }
        }
    }
}