namespace Advent
{
    public class Day06B : Day06
    {
        protected override int RunCore(string input)
        {
            return new MemoryBanks(input).Reallocate().LoopCount;
        }
    }
}