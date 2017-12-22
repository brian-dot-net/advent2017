namespace Advent
{
    using System.Linq;

    public sealed class Day01A : Day01
    {
        protected override int RunCore(Input input) => input.Raw.Zip(Rotate(input.Raw, 1), Matching).Sum();
    }
}