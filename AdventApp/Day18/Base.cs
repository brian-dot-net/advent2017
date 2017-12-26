namespace Advent.Day18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected sealed class Duet
        {
            private readonly Instructions instructions;

            public Duet(Input input)
            {
                this.instructions = new Instructions(input.Lines());
            }

            public int Run()
            {
                Memory memory = new Memory();
                this.instructions.Run(memory);
                return memory.LastRecovered;
            }

            private sealed class Memory
            {
                private readonly Dictionary<char, long> registers;

                private int lastSound;

                public Memory()
                {
                    this.registers = new Dictionary<char, long>();
                }

                public int LastRecovered { get; private set; }

                public int Instruction { get; private set; }

                public void PlaySound(char r)
                {
                    this.lastSound = (int)this.Get(r);
                    if (this.lastSound < 0)
                    {
                        throw new Exception("hmm");
                    }

                    this.Next();
                }

                public void Set(char r, Value v)
                {
                    this.Set(r, v, (x, y) => y);
                }

                public void Add(char r, Value v)
                {
                    this.Set(r, v, (x, y) => x + y);
                }

                public void Multiply(char r, Value v)
                {
                    this.Set(r, v, (x, y) => x * y);
                }

                public void Mod(char r, Value v)
                {
                    this.Set(r, v, (x, y) => x % y);
                }

                public bool Recover(char r)
                {
                    bool recovered = false;
                    if (this.Get(r) != 0)
                    {
                        this.LastRecovered = this.lastSound;
                        recovered = true;
                    }

                    this.Next();
                    return recovered;
                }

                public void Jump(char r, Value v)
                {
                    long inc = 1;
                    if (this.Get(r) > 0)
                    {
                        inc = this.Get(v);
                    }

                    this.Next((int)inc);
                }

                private long Get(Value v)
                {
                    if (v.Register > 0)
                    {
                        return this.Get(v.Register);
                    }

                    return v.Integer;
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

                private void Set(char r, Value v, Func<long, long, long> op)
                {
                    long x = this.Get(r);
                    long y = this.Get(v);
                    this.registers[r] = op(x, y);
                    this.Next();
                }

                private void Next(int inc = 1) => this.Instruction += inc;
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

                public int Integer => this.value;

                public char Register => this.isInteger ? '\0' : (char)this.value;
            }

            private sealed class Instructions
            {
                private readonly Instruction[] instructions;

                public Instructions(IEnumerable<Input> lines)
                {
                    this.instructions = lines.Select(Instruction.Parse).ToArray();
                }

                public void Run(Memory memory)
                {
                    while ((memory.Instruction >= 0) && (memory.Instruction < this.instructions.Length))
                    {
                        Instruction next = this.instructions[memory.Instruction];
                        if (next.Run(memory))
                        {
                            return;
                        }
                    }
                }

                private struct Instruction
                {
                    private readonly string opcode;
                    private readonly char x;
                    private readonly Value y;

                    private Instruction(string opcode, char x, Value y)
                    {
                        this.opcode = opcode;
                        this.x = x;
                        this.y = y;
                    }

                    public static Instruction Parse(Input input)
                    {
                        Input[] fields = input.Fields();
                        Value yVal = new Value();
                        if (fields.Length == 3)
                        {
                            yVal = new Value(fields[2]);
                        }

                        return new Instruction(fields[0].ToString(), fields[1].Character(), yVal);
                    }

                    public bool Run(Memory memory)
                    {
                        switch (opcode)
                        {
                            case "snd": return this.PlaySound(memory);
                            case "set": return this.Set(memory);
                            case "add": return this.Add(memory);
                            case "mul": return this.Multiply(memory);
                            case "mod": return this.Mod(memory);
                            case "rcv": return this.Recover(memory);
                            case "jgz": return this.Jump(memory);
                            default: throw new InvalidOperationException("Illegal instruction.");
                        }
                    }

                    private bool PlaySound(Memory memory)
                    {
                        memory.PlaySound(this.x);
                        return false;
                    }

                    private bool Set(Memory memory)
                    {
                        memory.Set(this.x, this.y);
                        return false;
                    }

                    private bool Add(Memory memory)
                    {
                        memory.Add(this.x, this.y);
                        return false;
                    }

                    private bool Multiply(Memory memory)
                    {
                        memory.Multiply(this.x, this.y);
                        return false;
                    }

                    private bool Mod(Memory memory)
                    {
                        memory.Mod(this.x, this.y);
                        return false;
                    }

                    private bool Recover(Memory memory)
                    {
                        return memory.Recover(this.x);
                    }

                    private bool Jump(Memory memory)
                    {
                        memory.Jump(this.x, this.y);
                        return false;
                    }
                }
            }
        }
    }
}