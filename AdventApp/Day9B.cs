namespace Advent
{
    public class Day9B : Day9
    {
        protected override int RunCore(string input)
        {
            return new Groups(input).CountGarbage();
        }
    }
}