namespace Advent.Day06
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new MemoryBanks(input).Reallocate().LoopCount;
    }
}