namespace Advent
{
    public class Day10A : Day10
    {
        protected override int RunCore(string input)
        {
            return new Knot(input).Hash();
        }
    }
}