namespace Advent.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected sealed class Particles
        {
            private readonly Particle[] particles;

            private KeyValuePair<int, int> closest;

            public Particles(Input input)
            {
                this.particles = input.Lines().Select(Particle.Parse).ToArray();
                this.closest = new KeyValuePair<int, int>(-1, -1);
            }

            public int Run()
            {
                int last = -1;
                for (int i = 0; i < this.particles.Length; ++i)
                {
                    last = this.RunOnce();
                }

                return last;
            }

            private int RunOnce()
            {
                while (true)
                {
                    KeyValuePair<int, int> nextClosest = this.RunNext();
                    if (nextClosest.Key == this.closest.Key)
                    {
                        return this.closest.Key;
                    }

                    this.closest = nextClosest;
                }
            }

            private KeyValuePair<int, int> RunNext()
            {
                KeyValuePair<int, int> nextClosest = new KeyValuePair<int, int>(-1, int.MaxValue);
                int n = this.particles.Length;
                for (int i = 0; i < n; ++i)
                {
                    Particle p = this.particles[i];
                    int d = p.Move();
                    if (d < nextClosest.Value)
                    {
                        nextClosest = new KeyValuePair<int, int>(i, d);
                    }
                }

                return nextClosest;
            }

            private sealed class Particle
            {
                private readonly Coords acceleration;

                private Coords position;
                private Coords velocity;

                private Particle(Coords position, Coords velocity, Coords acceleration)
                {
                    this.position = position;
                    this.velocity = velocity;
                    this.acceleration = acceleration;
                }

                public static Particle Parse(Input input)
                {
                    Coords[] parts = input.Fields(", ").Select(Coords.Parse).ToArray();
                    return new Particle(parts[0], parts[1], parts[2]);
                }

                public int Move()
                {
                    this.velocity += this.acceleration;
                    this.position += this.velocity;
                    return this.position.Distance;
                }

                private struct Coords
                {
                    private readonly int x;
                    private readonly int y;
                    private readonly int z;

                    private Coords(int x, int y, int z)
                    {
                        this.x = x;
                        this.y = y;
                        this.z = z;
                    }

                    public int Distance => Math.Abs(this.x) + Math.Abs(this.y) + Math.Abs(this.z);

                    public static Coords Parse(Input input)
                    {
                        string raw = input.Fields("=")[1].ToString();
                        raw = raw.Substring(1, raw.Length - 2);
                        int[] parts = new Input(raw).Fields(",").Select(f => f.Integer()).ToArray();
                        return new Coords(parts[0], parts[1], parts[2]);
                    }

                    public static Coords operator +(Coords a, Coords b) => new Coords(a.x + b.x, a.y + b.y, a.z + b.z);
                }
            }
        }
    }
}