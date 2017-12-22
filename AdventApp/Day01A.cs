namespace Advent
{
    using System.Linq;

    public sealed class Day01A : Day01
    {
        protected override int RunCore(Input input) => input.Chars().Zip(Rotate(input.Chars(), 1), Matching).Sum();
    }
}