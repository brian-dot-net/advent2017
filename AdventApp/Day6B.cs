namespace Advent
{
    public class Day6B : Day6
    {
        protected override int RunCore(string input)
        {
            return new MemoryBanks(input).Reallocate().LoopCount;
        }
    }
}