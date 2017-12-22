namespace Advent
{
    public class Day09B : Day09
    {
        protected override int RunCore(string input)
        {
            return new Groups(input).CountGarbage();
        }
    }
}