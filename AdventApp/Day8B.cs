namespace Advent
{
    public class Day8B : Day8
    {
        protected override int RunCore(string input)
        {
            return new Instructions(input).Run().MaxValueEver;
        }
    }
}