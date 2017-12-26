namespace Advent.Day09
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Groups(input).Score();
    }
}