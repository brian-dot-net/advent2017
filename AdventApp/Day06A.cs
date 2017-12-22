namespace Advent
{
    public class Day06A : Day06
    {
        protected override int RunCore(string input)
        {
            return new MemoryBanks(input).Reallocate().Count;
        }
    }
}