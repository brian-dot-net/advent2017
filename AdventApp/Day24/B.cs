namespace Advent.Day24
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new Bridge(input).BuildLongest();
    }
}