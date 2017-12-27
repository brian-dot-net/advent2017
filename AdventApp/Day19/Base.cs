namespace Advent.Day19
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Base : DayBase<string>
    {
        protected sealed class RoutingDiagram
        {
            private readonly List<char[]> diagram;
            private readonly int start;

            public RoutingDiagram(Input input)
            {
                this.diagram = new List<char[]>();
                foreach (Input line in input.Lines())
                {
                    char[] nextLine = line.ToString().ToCharArray();
                    this.diagram.Add(nextLine);
                    if (this.Width == 0)
                    {
                        this.Width = nextLine.Length;
                        this.start = Array.FindIndex(nextLine, c => c == '|');
                    }
                }
            }

            private int Width { get; set; }

            private int Height => this.diagram.Count;

            public string Run()
            {
                StringBuilder path = new StringBuilder();
                Queue<Step> steps = new Queue<Step>();
                steps.Enqueue(new Step(Direction.South, this.start, 0));
                while (steps.Count > 0)
                {
                    Step step = steps.Dequeue();
                    char c = this.diagram[step.Y][step.X];
                    if (c == '+')
                    {
                        this.Fork(step, steps);
                    }
                    else if (c != ' ')
                    {
                        steps.Enqueue(Next(step));
                        if (c >= 'A' && c <= 'Z')
                        {
                            path.Append(c);
                        }
                    }
                }

                return path.ToString();
            }

            private static Step Next(Step step)
            {
                switch (step.Dir)
                {
                    case Direction.North: return new Step(step.Dir, step.X, step.Y - 1);
                    case Direction.South: return new Step(step.Dir, step.X, step.Y + 1);
                    case Direction.East: return new Step(step.Dir, step.X + 1, step.Y);
                    default: return new Step(step.Dir, step.X - 1, step.Y);
                }
            }

            private void Fork(Step step, Queue<Step> steps)
            {
                switch (step.Dir)
                {
                    case Direction.North:
                    case Direction.South:
                        this.EnqueueIfValid(steps, step, new Step(Direction.East, 1, 0));
                        this.EnqueueIfValid(steps, step, new Step(Direction.West, -1, 0));
                        break;
                    default:
                        this.EnqueueIfValid(steps, step, new Step(Direction.South, 0, 1));
                        this.EnqueueIfValid(steps, step, new Step(Direction.North, 0, -1));
                        break;
                }
            }

            private void EnqueueIfValid(Queue<Step> steps, Step step, Step delta)
            {
                Step next = new Step(delta.Dir, step.X + delta.X, step.Y + delta.Y);
                if (this.IsValid(next.X, next.Y))
                {
                    steps.Enqueue(next);
                }
            }

            private bool IsValid(int x, int y) => (x >= 0) && (x < this.Width) && (y >= 0) && (y < this.Height);

            private enum Direction
            {
                North,
                South,
                East,
                West
            }

            private struct Step
            {
                private readonly Direction dir;
                private readonly int x;
                private readonly int y;

                public Step(Direction dir, int x, int y)
                {
                    this.dir = dir;
                    this.x = x;
                    this.y = y;
                }

                public Direction Dir => this.dir;

                public int X => this.x;

                public int Y => this.y;
            }
        }
    }
}