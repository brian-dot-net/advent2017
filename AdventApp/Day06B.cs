namespace Advent
{
    public class Day06B : Day06
    {
        protected override int RunCore(Input input)
        {
            return new MemoryBanks(input).Reallocate().LoopCount;
        }
    }
}