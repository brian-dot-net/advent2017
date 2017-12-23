namespace Advent
{
    public class Day14A : Day14
    {
        protected override int RunCore(Input input) => new Disk(input).UsedSquares();
    }
}