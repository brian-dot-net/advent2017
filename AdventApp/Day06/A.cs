namespace Advent.Day06
{
    public class A : Base
    {
        protected override int RunCore(Input input)
        {
            return new MemoryBanks(input).Reallocate().Count;
        }
    }
}