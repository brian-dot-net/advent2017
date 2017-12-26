namespace Advent.Day14
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Disk(input).UsedSquares();
    }
}