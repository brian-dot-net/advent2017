namespace Advent.Day09
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new Groups(input).CountGarbage();
    }
}