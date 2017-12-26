namespace Advent.Day02
{
    public sealed class A : Base
    {
        protected override int RunCore(Input input) => new Rows(input).SumOf(MaxDiff);
    }
}