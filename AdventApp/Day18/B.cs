namespace Advent.Day18
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new Duet(input).RunTwo();
    }
}