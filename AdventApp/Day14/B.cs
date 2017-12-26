namespace Advent.Day14
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new Disk(input).Regions();
    }
}