namespace Advent.Day03
{
    public sealed class A : Base
    {
        protected override int RunCore(Input input) => new Spiral(false).FirstExceeding(input.Integer() - 1).Distance;
    }
}