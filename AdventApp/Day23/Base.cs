namespace Advent.Day23
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected interface IRegisters
        {
            int MultiplyCount { get; }

            int H { get; }
        }

        protected sealed class Coprocessor
        {
            private readonly Instructions instructions;

            public Coprocessor(Input input)
            {
                this.instructions = new Instructions(input);
            }

            public IRegisters Run(bool debug)
            {
                Registers registers = new Registers(debug);
                this.instructions.Run(registers);
                return registers;
            }

            private sealed class Instructions
            {
                private readonly Instruction[] instructions;

                public Instructions(Input input)
                {
                    this.instructions = input.Lines().Select(Instruction.Parse).ToArray();
                }

                public void Run(Registers registers)
                {
                    int n = this.instructions.Length;
                    while ((registers.Instruction >= 0) && (registers.Instruction < n))
                    {
                        Instruction instruction = this.instructions[registers.Instruction];
                        instruction.Run(registers);
                    }
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

                    public void Run(Registers registers)
                    {
                        switch (this.opcode)
                        {
                            case "set":
                                registers.Set(this.x, this.y);
                                break;
                            case "sub":
                                registers.Subtract(this.x, this.y);
                                break;
                            case "mul":
                                registers.Multiply(this.x, this.y);
                                break;
                            case "jnz":
                                registers.Jump(this.x, this.y);
                                break;
                            default:
                                throw new InvalidOperationException("Illegal instruction.");
                        }
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

            private sealed class Registers : IRegisters
            {
                private readonly Dictionary<char, long> registers;
                private readonly bool debug;

                public Registers(bool debug)
                {
                    this.registers = new Dictionary<char, long>();
                    this.debug = debug;
                    if (!this.debug)
                    {
                        this.registers['a'] = 1;
                    }
                }

                public int Instruction { get; private set; }

                public int MultiplyCount { get; private set; }

                public int H => (int)this.Get('h');

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
                    ++this.MultiplyCount;
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