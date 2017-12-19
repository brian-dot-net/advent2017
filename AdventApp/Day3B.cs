namespace Advent
{
    using System.Linq;

    public sealed class Day3B : Day3
    {
        protected override int RunCore(string input)
        {
            int n = int.Parse(input);
            return new Spiral(true).FirstCell(c => c.Value > n).Value;
        }
    }
}