namespace Advent
{
    public class Day11A : Day11
    {
        protected override int RunCore(Input input) => new HexGrid(input).Distance;
    }
}