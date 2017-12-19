namespace Advent
{
    using System.Linq;

    public sealed class Day1A : Day1
    {
        protected override int RunCore(string input) => input.Zip(Rotate(input, 1), Matching).Sum();
    }
}