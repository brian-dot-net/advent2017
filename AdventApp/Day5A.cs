using System.Linq;

namespace Advent
{
    public class Day5A : Day5
    {
        protected override int RunCore(string input)
        {
            int[] jumps = Lines.From(input).Select(n => int.Parse(n)).ToArray();
            int i = 0;
            int total = 0;
            while (i < jumps.Length)
            {
                int offset = jumps[i];
                ++jumps[i];
                ++total;
                i += offset;
            }

            return total;
        }
    }
}