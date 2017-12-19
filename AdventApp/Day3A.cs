namespace Advent
{
    using System.Linq;

    public sealed class Day3A : Day3
    {
        protected override int RunCore(string input)
        {
            int n = int.Parse(input);
            return new Spiral(false).FirstCell(c => c.Value == n).Distance;
        }
    }
}