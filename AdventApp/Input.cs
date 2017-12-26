﻿namespace Advent
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public struct Input
    {
        private readonly string raw;

        public Input(string raw)
        {
            this.raw = raw;
        }

        public int Length => this.raw.Length;

        public int Integer() => int.Parse(this.raw);

        public char Character() => this.raw.Single();

        public byte Byte() => byte.Parse(this.raw);

        public IEnumerable<char> Chars() => this.raw;

        public Input[] Fields() => this.Fields(" ");

        public Input[] Fields(string separator) => this.Fields(separator, int.MaxValue);

        public Input[] Fields(int max) => this.Fields(" ", max);

        public Input[] Fields(string separator, int max) => this.Split(separator, max).Select(s => new Input(s)).ToArray();

        public byte[] AsciiBytes() => Encoding.ASCII.GetBytes(this.raw);

        public bool IsInteger() => int.TryParse(this.raw, out int x);

        public override string ToString() => this.raw;

        public IEnumerable<Input> Lines()
        {
            using (StringReader sr = new StringReader(this.raw))
            {
                string next;
                do
                {
                    next = sr.ReadLine();
                    if (next != null)
                    {
                        yield return new Input(next);
                    }
                }
                while (next != null);
            }
        }

        private string[] Split(string separator, int max) => this.raw.Split(new string[] { separator }, max, StringSplitOptions.None);
    }
}