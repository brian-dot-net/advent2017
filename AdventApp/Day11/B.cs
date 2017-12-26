namespace Advent.Day11
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new HexGrid(input).MaxDistance;
    }
}