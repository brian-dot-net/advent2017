namespace Advent.Day06
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new MemoryBanks(input).Reallocate().Count;
    }
}