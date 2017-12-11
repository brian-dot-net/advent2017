namespace Advent
{
    using System;

    public abstract class Day3 : ICanRun
    {
        public string DefaultInput
        {
            get => @"265149";
        }

        public int Run(string input)
        {
            return this.RunCore(input);
        }

        protected abstract int RunCore(string input);

        protected sealed class Ring
        {
            private readonly int index;

            public Ring(int index)
            {
                this.index = index;
            }

            public int Max => 4 * this.index * (this.index + 1) + 1;

            private int Min => 4 * this.index * (this.index - 1) + 2;

            private int Count => this.Max - this.Min + 1;

            private int QuadLength => this.index * 2;

            public int Distance(int n)
            {
                if (this.index == 0)
                {
                    return 0;
                }

                int i = n - this.Min;
                int q = 4 * i / this.Count;
                int qi = i - q * this.QuadLength;
                switch ((Quadrant)q)
                {
                    case Quadrant.Right:
                        return QuadDistance(qi, P(1, 0), P(0, 1));
                    case Quadrant.Upper:
                        return QuadDistance(qi, P(0, 1), P(-1, 0));
                    case Quadrant.Left:
                        return QuadDistance(qi, P(-1, 0), P(0, -1));
                    case Quadrant.Lower:
                        return QuadDistance(qi, P(0, -1), P(1, 0));
                    default:
                        throw new NotSupportedException();
                }
            }

            private static Pair P(int x, int y) => new Pair(x, y);

            public Ring Next()
            {
                return new Ring(this.index + 1);
            }

            private static int QuadDistance(int qi, Pair start, Pair inc) => (start + qi * inc).Distance;

            private enum Quadrant
            {
                Right,
                Upper,
                Left,
                Lower
            }

            private struct Pair
            {
                private readonly int x;
                private readonly int y;

                public Pair(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }

                public int Distance => Math.Abs(x) + Math.Abs(y);

                public static Pair operator +(Pair p1, Pair p2) => new Pair(p1.x + p2.x, p1.y + p2.y);

                public static Pair operator *(int n, Pair p) => new Pair(p.x * n, p.y * n);
            }
        }
    }
}