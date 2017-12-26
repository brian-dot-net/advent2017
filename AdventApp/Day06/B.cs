namespace Advent.Day06
{
    public class B : Base
    {
        protected override int RunCore(Input input)
        {
            return new MemoryBanks(input).Reallocate().LoopCount;
        }
    }
}