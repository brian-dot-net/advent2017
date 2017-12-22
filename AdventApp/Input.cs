namespace Advent
{
    using System.Collections.Generic;
    using System.IO;

    public struct Input
    {
        private readonly string raw;

        public Input(string raw)
        {
            this.raw = raw;
        }

        public string Raw => this.raw;

        public int Integer()
        {
            return int.Parse(this.Raw);
        }

        public IEnumerable<string> Lines()
        {
            using (StringReader sr = new StringReader(this.Raw))
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