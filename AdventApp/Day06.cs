﻿namespace Advent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Day06 : DayBase<int>
    {
        protected sealed class MemoryBanks
        {
            private readonly int length;
            private readonly byte[] banks;

            public MemoryBanks(Input input)
            {
                List<byte> inputBanks = input.Fields().Select(f => f.Byte()).ToList();
                this.length = inputBanks.Count;
                for (int i = inputBanks.Count; i < 16; ++i)
                {
                    inputBanks.Add(0);
                }

                this.banks = inputBanks.ToArray();
            }

            public Cycles Reallocate()
            {
                Cycles cycles = new Cycles();
                do
                {
                    if (!cycles.Add(this.banks))
                    {
                        return cycles;
                    }

                    this.ReallocateInner();
                }
                while (true);
            }

            private void ReallocateInner()
            {
                byte max = 0;
                int start = 0;
                for (int i = 0; i < this.length; ++i)
                {
                    if (this.banks[i] > max)
                    {
                        max = this.banks[i];
                        start = i + 1;
                    }
                }

                this.banks[start - 1] = 0;
                byte share = (byte)(max / this.length);
                int excess = max % this.length;

                for (int i = 0; i < this.length; ++i)
                {
                    int k = (start + i) % this.length;
                    byte add = share;
                    if (excess > 0)
                    {
                        ++add;
                        --excess;
                    }

                    this.banks[k] += add;
                }
            }
        }

        protected sealed class Cycles
        {
            private readonly Dictionary<Guid, int> states;

            public Cycles()
            {
                this.states = new Dictionary<Guid, int>();
            }

            public int LoopCount { get; private set; }

            public int Count { get; private set; }

            public bool Add(byte[] banks)
            {
                Guid key = new Guid(banks);
                int firstCycle;
                if (this.states.TryGetValue(key, out firstCycle))
                {
                    this.LoopCount = 1 + this.Count - firstCycle;
                    return false;
                }

                this.states.Add(key, ++this.Count);
                return true;
            }
        }
    }
}