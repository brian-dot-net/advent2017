﻿namespace Advent.Day05
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected sealed class JumpTable : IEnumerable<int>
        {
            private readonly IReadOnlyList<int> jumps;
            private readonly Func<int, int> incJump;

            public JumpTable(Input input, Func<int, int> incJump)
            {
                this.jumps = input.Lines().Select(l => l.Integer()).ToList().AsReadOnly();
                this.incJump = incJump;
            }

            public IEnumerator<int> GetEnumerator() => new JumpEnumerator(this.jumps.ToArray(), this.incJump);

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

            private sealed class JumpEnumerator : IEnumerator<int>
            {
                private readonly int[] jumps;
                private readonly Func<int, int> incJump;

                public JumpEnumerator(int[] jumps, Func<int, int> incJump)
                {
                    this.jumps = jumps;
                    this.incJump = incJump;
                }

                public int Current { get; private set; }

                object IEnumerator.Current => this.Current;

                public void Dispose()
                {
                }

                public bool MoveNext()
                {
                    if (this.Current >= this.jumps.Length)
                    {
                        return false;
                    }

                    int offset = this.jumps[this.Current];
                    this.jumps[this.Current] += this.incJump(offset);
                    this.Current += offset;

                    return true;
                }

                public void Reset()
                {
                    this.Current = 0;
                }
            }
        }
    }
}