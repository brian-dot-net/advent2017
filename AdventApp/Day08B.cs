namespace Advent
{
    public class Day08B : Day08
    {
        protected override int RunCore(string input)
        {
            return new Instructions(input).Run().MaxValueEver;
        }
    }
}