namespace Advent.Day01
{
    using System.Linq;

    public sealed class A : Base
    {
        protected override int RunCore(Input input) => input.Chars().Zip(Rotate(input.Chars(), 1), Matching).Sum();
    }
}