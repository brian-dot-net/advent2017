namespace Advent
{
    using System.Linq;

    public sealed class Day01B : Day01
    {
        protected override int RunCore(Input input) => input.Chars().Zip(Rotate(input.Chars(), input.Length / 2), Matching).Sum();
    }
}