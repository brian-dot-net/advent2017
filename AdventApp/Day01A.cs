namespace Advent
{
    using System.Linq;

    public sealed class Day01A : Day01
    {
        protected override int RunCore(string input) => input.Zip(Rotate(input, 1), Matching).Sum();
    }
}