namespace Advent.Day02
{
    public sealed class B : Base
    {
        protected override int RunCore(Input input) => new Rows(input).SumOf(DivideTwo);
    }
}