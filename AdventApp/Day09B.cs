namespace Advent
{
    public class Day09B : Day09
    {
        protected override int RunCore(Input input)
        {
            return new Groups(input).CountGarbage();
        }
    }
}