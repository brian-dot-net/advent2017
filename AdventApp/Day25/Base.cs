namespace Advent.Day25
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected sealed class TuringMachine
        {
            private readonly char start;
            private readonly int diag;
            private readonly States states;

            public TuringMachine(Input input)
            {
                Lines lines = new Lines(input);
                this.start = lines.NextBetween("state ", ".").Character();
                this.diag = lines.NextBetween("after ", " steps.").Integer();
                lines.Next();
                this.states = new States(lines);
            }

            public int Run()
            {
                Tape tape = new Tape();
                char current = this.start;
                for (int i = 0; i < diag; ++i)
                {
                    current = this.states.Run(current, tape);
                }

                return tape.Checksum();
            }

            private sealed class Tape
            {
                private readonly TapeSegments segments;

                private int i;

                public Tape()
                {
                    this.segments = new TapeSegments();
                }

                public char Run(Rule rule0, Rule rule1)
                {
                    Rule rule = this.Get() ? rule1 : rule0;
                    return rule.Run(this);
                }

                public int Checksum() => this.segments.Checksum();

                public void Set(bool one) => this.segments.Set(this.i, one);

                public void Move(bool right) => this.i += right ? 1 : -1;

                private bool Get() => this.segments.Get(this.i);

                private sealed class TapeSegments
                {
                    private readonly Dictionary<int, TapeSegment> segments;

                    public TapeSegments()
                    {
                        this.segments = new Dictionary<int, TapeSegment>();
                    }

                    public int Checksum() => this.segments.Values.Select(s => s.Checksum()).Sum();

                    public void Set(int i, bool one)
                    {
                        KeyValuePair<int, int> kv = Index(i);
                        TapeSegment segment = this.GetSegment(kv.Key);
                        segment[kv.Value] = one;
                    }

                    public bool Get(int i)
                    {
                        KeyValuePair<int, int> kv = Index(i);
                        TapeSegment segment = this.GetSegment(kv.Key);
                        return segment[kv.Value];
                    }

                    private TapeSegment GetSegment(int k)
                    {
                        TapeSegment segment;
                        if (!this.segments.TryGetValue(k, out segment))
                        {
                            segment = new TapeSegment();
                            this.segments.Add(k, segment);
                        }

                        return segment;
                    }

                    private static KeyValuePair<int, int> Index(int i)
                    {
                        const int N = TapeSegment.Size;
                        int j;
                        int k;
                        if (i < 0)
                        {
                            j = ((i + 1) / N) - 1;
                            k = ((i + 1) % N) - 1 + N;
                        }
                        else
                        {
                            j = i / N;
                            k = i % N;
                        }

                        return new KeyValuePair<int, int>(j, k);
                    }

                }

                private sealed class TapeSegment
                {
                    public const int Size = sizeof(ulong) * 8;

                    private static readonly OneBitsTable Table = new OneBitsTable();

                    private ulong bits;

                    public bool this[int i]
                    {
                        get
                        {
                            int s = i % Size;
                            return ((this.bits >> s) & 1) == 1;
                        }

                        set
                        {
                            int s = i % Size;
                            if (value)
                            {
                                this.bits |= 1UL << s;
                            }
                            else
                            {
                                this.bits &= ~(1UL << s);
                            }
                        }
                    }

                    public int Checksum()
                    {
                        return
                            this.CountBitsN(0) +
                            this.CountBitsN(1) +
                            this.CountBitsN(2) +
                            this.CountBitsN(3) +
                            this.CountBitsN(4) +
                            this.CountBitsN(5) +
                            this.CountBitsN(6) +
                            this.CountBitsN(7);
                    }

                    private int CountBitsN(int n) => Table[(byte)(0xFF & (this.bits >> (8 * n)))];
                }
            }

            private sealed class Lines
            {
                private readonly IEnumerator<Input> lines;

                public Lines(Input input)
                {
                    this.lines = input.Lines().GetEnumerator();
                }

                public Input NextBetween(string left, string right)
                {
                    Input next = this.Next();
                    if (next.Length > 0)
                    {
                        return next.Fields(left)[1].Fields(right)[0];
                    }

                    return Null;
                }

                public Input Next()
                {
                    if (this.lines.MoveNext())
                    {
                        return this.lines.Current;
                    }

                    return Null;
                }

                private static Input Null => new Input(string.Empty);
            }

            private struct Rule
            {
                private readonly char nextState;
                private readonly bool writeOne;
                private readonly bool moveRight;

                public Rule(Lines lines, int expected)
                {
                    int actual = lines.NextBetween("current value is ", ":").Integer();
                    if (actual != expected)
                    {
                        throw new InvalidOperationException("Expected rule " + expected + " but read " + actual + ".");
                    }

                    this.writeOne = lines.NextBetween("the value ", ".").Integer() == 1;
                    this.moveRight = lines.NextBetween("to the ", ".").ToString() == "right";
                    this.nextState = lines.NextBetween("state ", ".").Character();
                }

                public char Run(Tape tape)
                {
                    tape.Set(this.writeOne);
                    tape.Move(this.moveRight);
                    return this.nextState;
                }
            }

            private sealed class States
            {
                private readonly Dictionary<char, State> states;

                public States(Lines lines)
                {
                    this.states = new Dictionary<char, State>();
                    while (this.ReadNext(lines))
                    {
                    }
                }

                public char Run(char state, Tape tape) => this.states[state].Run(tape);

                private bool ReadNext(Lines lines)
                {
                    Input input = lines.NextBetween("state ", ":");
                    if (input.Length == 0)
                    {
                        return false;
                    }

                    char state = input.Character();
                    this.states.Add(state, new State(lines));
                    lines.Next();
                    return true;
                }

                private sealed class State
                {
                    private readonly Rule rule0;
                    private readonly Rule rule1;

                    public State(Lines lines)
                    {
                        this.rule0 = new Rule(lines, 0);
                        this.rule1 = new Rule(lines, 1);
                    }

                    public char Run(Tape tape)
                    {
                        return tape.Run(rule0, rule1);
                    }
                }
            }
        }
    }
}