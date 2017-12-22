namespace Advent
{
    using System.Linq;

    public sealed class Day01B : Day01
    {
        protected override int RunCore(Input input) => input.Raw.Zip(Rotate(input.Raw, input.Raw.Length / 2), Matching).Sum();
    }
}