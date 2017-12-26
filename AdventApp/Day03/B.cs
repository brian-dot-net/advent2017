namespace Advent.Day03
{
    public sealed class B : Base
    {
        protected override int RunCore(Input input) => new Spiral(true).FirstExceeding(input.Integer()).Value;
    }
}