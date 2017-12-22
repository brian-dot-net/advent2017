namespace Advent
{
    using System.Linq;

    public sealed class Day01B : Day01
    {
        protected override int RunCore(string input) => input.Zip(Rotate(input, input.Length / 2), Matching).Sum();
    }
}