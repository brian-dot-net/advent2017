namespace Advent
{
    using System;

    public abstract class Day11 : DayBase<int>
    {
        protected sealed class HexGrid
        {
            private Pair location;

            public HexGrid(Input input)
            {
                foreach (Input move in input.Fields(','))
                {
                    this.Move(Read(move.ToString()));
                    this.MaxDistance = Math.Max(this.location.HexDistance, this.MaxDistance);
                }
            }

            public int Distance => this.location.HexDistance;

            public int MaxDistance { get; private set; }

            private static Pair P(int x, int y) => new Pair(x, y);
            
            private Pair Read(string move)
            {
                switch (move)
                {
                    case "n": return P(0, 2);
                    case "ne": return P(1, 1);
                    case "se": return P(1, -1);
                    case "s": return P(0, -2);
                    case "sw": return P(-1, -1);
                    default: return P(-1, 1);
                }
            }

            private void Move(Pair dir) => this.location += dir;

            private struct Pair
            {
                private readonly int x;
                private readonly int y;

                public Pair(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }

                public static Pair operator +(Pair a, Pair b) => new Pair(a.x + b.x, a.y + b.y);

                public int HexDistance
                {
                    get
                    {
                        int top = Math.Abs(this.y);
                        int right = Math.Abs(this.x);
                        int diagonalSteps = 0;

                        if (right > top)
                        {
                            diagonalSteps += top;
                            right -= top;
                            top = 0;
                        }
                        else
                        {
                            diagonalSteps += right;
                            top -= right;
                            right = 0;
                        }

                        return diagonalSteps + (top / 2) + right;
                    }
                }
            }
        }
    }
}