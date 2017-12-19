namespace Advent
{
    using System.Collections.Generic;
    using System.IO;

    internal static class Lines
    {
        public static IEnumerable<string> From(string input)
        {
            using (StringReader sr = new StringReader(input))
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