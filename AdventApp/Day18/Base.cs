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
                Memory p0 = new Memory(0);
                this.instructions.Run(p0, null);
                return (int)p0.LastReceived;
            }

            public int RunTwo()
            {
                Memory p0 = new Memory(0);
                Memory p1 = new Memory(1);
                this.instructions.Run(p0, p1);
                return p1.SendCount;
            }

            private sealed class Memory
            {
                private readonly long id;
                private readonly Dictionary<char, long> registers;
                private readonly Queue<long> queue;

                private bool receiving;
                private long lastSend;

                public Memory(long id)
                {
                    this.id = id;
                    this.registers = new Dictionary<char, long>();
                    this.registers['p'] = id;
                    this.queue = new Queue<long>();
                }

                public int SendCount { get; private set; }

                public long LastReceived { get; private set; }

                public int Instruction { get; private set; }

                public void Send(Value x, Memory waiting)
                {
                    this.lastSend = this.Get(x);
                    if (waiting != null)
                    {
                        waiting.Enqueue(this.lastSend);
                    }

                    ++this.SendCount;
                    this.JumpRelative(1);
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

                public bool Receive(char r, Memory waiting)
                {
                    if (waiting == null)
                    {
                        bool received = false;
                        if (this.Get(r) != 0)
                        {
                            this.LastReceived = this.lastSend;
                            received = true;
                        }

                        this.JumpRelative(1);
                        return received;
                    }

                    return this.Receive(r);
                }

                private bool Receive(char r)
                {
                    bool mustReceive = this.receiving;
                    this.receiving = false;
                    if (this.queue.Count > 0)
                    {
                        this.Set(r, this.queue.Dequeue());
                        return false;
                    }
                    else if (mustReceive)
                    {
                        this.JumpAbsolute(-1);
                        return false;
                    }
                    else
                    {
                        this.receiving = true;
                        return true;
                    }
                }

                public void Jump(Value x, Value v)
                {
                    long inc = 1;
                    if (this.Get(x) > 0)
                    {
                        inc = this.Get(v);
                    }

                    this.JumpRelative((int)inc);
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
                    this.Set(r, op(x, y));
                }

                private void Set(char r, long y)
                {
                    this.registers[r] = y;
                    this.JumpRelative(1);
                }

                private void Enqueue(long y)
                {
                    this.queue.Enqueue(y);
                }

                private void JumpRelative(int inc) => this.JumpAbsolute(this.Instruction + inc);

                private void JumpAbsolute(int i) => this.Instruction = i;
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

                public void Run(Memory p0, Memory p1)
                {
                    if (p1 == null)
                    {
                        this.RunOne(p0, null);
                    }
                    else
                    {
                        this.RunTwo(p0, p1);
                    }
                }

                private bool RunOne(Memory running, Memory waiting)
                {
                    while ((running.Instruction >= 0) && (running.Instruction < this.instructions.Length))
                    {
                        Instruction next = this.instructions[running.Instruction];
                        if (next.Run(running, waiting))
                        {
                            return true;
                        }
                    }

                    return false;
                }

                private void RunTwo(Memory p0, Memory p1)
                {
                    Queue<Tuple<Memory, Memory>> ps = new Queue<Tuple<Memory, Memory>>();
                    ps.Enqueue(Tuple.Create(p0, p1));
                    while (ps.Count > 0)
                    {
                        Tuple<Memory, Memory> p = ps.Dequeue();
                        if (this.RunOne(p.Item1, p.Item2))
                        {
                            ps.Enqueue(Tuple.Create(p.Item2, p.Item1));
                        }
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
                        Value yVal = new Value();
                        if (fields.Length == 3)
                        {
                            yVal = new Value(fields[2]);
                        }

                        return new Instruction(fields[0].ToString(), new Value(fields[1]), yVal);
                    }

                    public bool Run(Memory running, Memory waiting)
                    {
                        switch (opcode)
                        {
                            case "snd": return this.Send(running, waiting);
                            case "set": return this.Set(running);
                            case "add": return this.Add(running);
                            case "mul": return this.Multiply(running);
                            case "mod": return this.Mod(running);
                            case "rcv": return this.Receive(running, waiting);
                            case "jgz": return this.Jump(running);
                            default: throw new InvalidOperationException("Illegal instruction.");
                        }
                    }

                    private bool Send(Memory running, Memory waiting)
                    {
                        running.Send(this.x, waiting);
                        return false;
                    }

                    private bool Set(Memory memory)
                    {
                        memory.Set(this.x.Register, this.y);
                        return false;
                    }

                    private bool Add(Memory memory)
                    {
                        memory.Add(this.x.Register, this.y);
                        return false;
                    }

                    private bool Multiply(Memory memory)
                    {
                        memory.Multiply(this.x.Register, this.y);
                        return false;
                    }

                    private bool Mod(Memory memory)
                    {
                        memory.Mod(this.x.Register, this.y);
                        return false;
                    }

                    private bool Receive(Memory running, Memory waiting)
                    {
                        return running.Receive(this.x.Register, waiting);
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