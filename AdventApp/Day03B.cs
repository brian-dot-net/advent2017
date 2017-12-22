namespace Advent
{
    using System.Linq;

    public sealed class Day03B : Day03
    {
        protected override int RunCore(string input)
        {
            int n = int.Parse(input);
            return new Spiral(true).FirstCell(c => c.Value > n).Value;
        }
    }
}