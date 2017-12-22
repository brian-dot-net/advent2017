namespace Advent
{
    public class Day08B : Day08
    {
        protected override int RunCore(Input input)
        {
            return new Instructions(input).Run().MaxValueEver;
        }
    }
}