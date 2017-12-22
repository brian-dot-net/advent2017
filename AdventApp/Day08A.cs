namespace Advent
{
    public class Day08A : Day08
    {
        protected override int RunCore(Input input)
        {
            return new Instructions(input).Run().MaxValue();
        }
    }
}