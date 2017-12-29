namespace Advent.Day24
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Bridge(input).Build();
    }
}