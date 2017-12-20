﻿namespace Advent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Day6 : ICanRun
    {
        public string DefaultInput => @"11 11 13 7 0 15 5 5 4 4 1 1 7 1 15 11";

        public int Run(string input) => this.RunCore(input);

        protected abstract int RunCore(string input);

        protected sealed class MemoryBanks
        {
            private readonly int length;
            private readonly byte[] banks;

            public MemoryBanks(string input)
            {
                List<byte> inputBanks = input.Split().Select(n => byte.Parse(n)).ToList();
                this.length = inputBanks.Count;
                for (int i = inputBanks.Count; i < 16; ++i)
                {
                    inputBanks.Add(0);
                }

                this.banks = inputBanks.ToArray();
            }

            public int Reallocate()
            {
                HashSet<Guid> states = new HashSet<Guid>();
                int cycles = 0;
                do
                {
                    if (!states.Add(new Guid(this.banks)))
                    {
                        return cycles;
                    }

                    this.ReallocateInner();
                    ++cycles;
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

    }
}