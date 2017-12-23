namespace Advent
{
    public class Day11B : Day11
    {
        protected override int RunCore(Input input) => new HexGrid(input).MaxDistance;
    }
}