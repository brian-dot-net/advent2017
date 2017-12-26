﻿namespace Advent.Day08
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected sealed class Instructions
        {
            private readonly Instruction[] items;

            public Instructions(Input input)
            {
                this.items = input.Lines().Select(Instruction.Parse).ToArray();
            }

            public Registers Run()
            {
                Registers registers = new Registers();
                foreach (Instruction i in this.items)
                {
                    i.Run(registers);
                }

                return registers;
            }

            private sealed class Instruction
            {
                private readonly string register;
                private readonly int inc;
                private readonly Condition cond;

                private Instruction(string register, int inc, Condition cond)
                {
                    this.register = register;
                    this.inc = inc;
                    this.cond = cond;
                }

                public static Instruction Parse(Input input)
                {
                    Input[] fields = input.Fields(4);
                    int sign = fields[1].ToString() == "inc" ? 1 : -1;
                    return new Instruction(fields[0].ToString(), sign * fields[2].Integer(), Condition.Parse(fields[3].ToString()));
                }

                public void Run(Registers registers)
                {
                    if (this.cond.IsTrue(registers))
                    {
                        registers.Increment(this.register, this.inc);
                    }
                }

                private sealed class Condition
                {
                    private readonly string register;
                    private readonly string op;
                    private readonly int right;

                    private Condition(string register, string op, int right)
                    {
                        this.register = register;
                        this.op = op;
                        this.right = right;
                    }

                    public static Condition Parse(string input)
                    {
                        string[] fields = input.Split();
                        return new Condition(fields[1], fields[2], int.Parse(fields[3]));
                    }

                    public bool IsTrue(Registers registers)
                    {
                        int left = registers.Get(this.register);
                        switch (this.op)
                        {
                            case ">": return left > right;
                            case "<": return left < right;
                            case "<=": return left <= right;
                            case ">=": return left >= right;
                            case "==": return left == right;
                            default: return left != right;
                        }
                    }
                }
            }
        }

        protected sealed class Registers
        {
            private readonly Dictionary<string, int> values;

            public Registers()
            {
                this.values = new Dictionary<string, int>();
            }

            public int MaxValueEver { get; private set; }

            public int MaxValue() => this.values.Values.Max();

            public void Increment(string register, int value)
            {
                int start = this.Get(register);
                this.values[register] = this.CheckMax(start + value);
            }

            public int Get(string register)
            {
                int value;
                if (!this.values.TryGetValue(register, out value))
                {
                    value = 0;
                    this.values.Add(register, value);
                }

                return value;
            }

            private int CheckMax(int value)
            {
                if (value > this.MaxValueEver)
                {
                    this.MaxValueEver = value;
                }

                return value;
            }
        }
    }
}