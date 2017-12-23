namespace Advent
{
    public class Day14B : Day14
    {
        protected override int RunCore(Input input) => new Disk(input).Regions();
    }
}