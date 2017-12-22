namespace Advent
{
    using System.Collections.Generic;
    using System.IO;
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

        public IEnumerable<char> Chars() => this.raw;

        public string[] Fields() => this.raw.Split();

        public byte[] AsciiBytes() => Encoding.ASCII.GetBytes(this.raw);

        public override string ToString() => this.raw;

        public IEnumerable<string> Lines()
        {
            using (StringReader sr = new StringReader(this.raw))
            {
                string next;
                do
                {
                    next = sr.ReadLine();
                    if (next != null)
                    {
                        yield return next;
                    }
                }
                while (next != null);
            }
        }
    }
}