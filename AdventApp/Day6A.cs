namespace Advent
{
    public class Day6A : Day6
    {
        protected override int RunCore(string input)
        {
            return new MemoryBanks(input).Reallocate();
        }
    }
}