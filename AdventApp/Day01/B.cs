namespace Advent.Day01
{
    using System.Linq;

    public sealed class B : Base
    {
        protected override int RunCore(Input input) => input.Chars().Zip(Rotate(input.Chars(), input.Length / 2), Matching).Sum();
    }
}