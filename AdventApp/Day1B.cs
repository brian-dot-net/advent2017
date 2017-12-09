namespace Advent
{
    using System.Linq;

    public sealed class Day1B : Day1
    {
        protected override int RunCore(string input)
        {
            return input.Zip(Rotate(input, input.Length / 2), Matching).Sum();
        }
    }
}