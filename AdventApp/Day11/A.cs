namespace Advent.Day11
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new HexGrid(input).Distance;
    }
}