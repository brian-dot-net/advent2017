namespace Advent.Day23
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected sealed class Coprocessor
        {
            private readonly Instructions instructions;

            public Coprocessor(Input input)
            {
                this.instructions = new Instructions(input);
            }

            public int Run()
            {
                return this.instructions.Run(new Registers());
            }

            private sealed class Instructions
            {
                private readonly Instruction[] instructions;

                public Instructions(Input input)
                {
                    this.instructions = input.Lines().Select(Instruction.Parse).ToArray();
                }

                public int Run(Registers registers)
                {
                    int n = this.instructions.Length;
                    int m = 0;
                    while ((registers.Instruction >= 0) && (registers.Instruction < n))
                    {
                        m += this.instructions[registers.Instruction].Run(registers);
                    }

                    return m;
                }

                private struct Instruction
                {
                    private readonly string opcode;
                    private readonly Value x;
                    private readonly Value y;

                    private Instruction(string opcode, Value x, Value y)
                    {
                        this.opcode = opcode;
                        this.x = x;
                        this.y = y;
                    }

                    public static Instruction Parse(Input input)
                    {
                        Input[] fields = input.Fields();
                        return new Instruction(fields[0].ToString(), new Value(fields[1]), new Value(fields[2]));
                    }

                    public int Run(Registers registers)
                    {
                        switch (this.opcode)
                        {
                            case "set": return this.Set(registers);
                            case "sub": return this.Subtract(registers);
                            case "mul": return this.Multiply(registers);
                            case "jnz": return this.Jump(registers);
                            default: throw new InvalidOperationException("Illegal instruction.");
                        }
                    }

                    private int Set(Registers registers)
                    {
                        registers.Set(this.x, this.y);
                        return 0;
                    }

                    private int Subtract(Registers registers)
                    {
                        registers.Subtract(this.x, this.y);
                        return 0;
                    }

                    private int Multiply(Registers registers)
                    {
                        registers.Multiply(this.x, this.y);
                        return 1;
                    }

                    private int Jump(Registers registers)
                    {
                        registers.Jump(this.x, this.y);
                        return 0;
                    }
                }
            }

            private struct Value
            {
                private readonly int value;
                private readonly bool isInteger;

                public Value(Input input)
                {
                    this.isInteger = input.IsInteger();
                    this.value = this.isInteger ? input.Integer() : input.Character();
                }

                public char Character => (char)this.value;

                public bool IsInteger => this.isInteger;

                public int Integer => this.value;
            }

            private sealed class Registers
            {
                private readonly Dictionary<char, long> registers;

                public Registers()
                {
                    this.registers = new Dictionary<char, long>();
                }

                public int Instruction { get; private set; }

                public void Set(Value x, Value y)
                {
                    this.Set(x.Character, this.Get(y));
                }

                public void Subtract(Value x, Value y)
                {
                    this.Set(x.Character, this.Get(x) - this.Get(y));
                }

                public void Multiply(Value x, Value y)
                {
                    this.Set(x.Character, this.Get(x) * this.Get(y));
                }

                public void Jump(Value x, Value y)
                {
                    long v = this.Get(x);
                    long inc = (v == 0) ? 1 : this.Get(y);
                    this.Jump((int)inc);
                }

                private void Set(char r, long y)
                {
                    this.registers[r] = y;
                    this.Jump(1);
                }

                private long Get(Value v)
                {
                    if (v.IsInteger)
                    {
                        return v.Integer;
                    }

                    return this.Get(v.Character);
                }

                private long Get(char r)
                {
                    long value;
                    if (!this.registers.TryGetValue(r, out value))
                    {
                        value = 0;
                        this.registers.Add(r, value);
                    }

                    return value;
                }

                private void Jump(int inc) => this.Instruction += inc;
            }
        }
    }
}