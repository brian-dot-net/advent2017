namespace Advent
{
    using System.Linq;

    public sealed class Day3A : Day3
    {
        protected override int RunCore(string input)
        {
            int n = int.Parse(input);
            Spiral spiral = new Spiral();
            return spiral.Cells(false).First(c => c.Value == n).Distance;
        }
    }
}