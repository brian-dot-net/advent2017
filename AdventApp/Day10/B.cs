namespace Advent.Day10
{
    public class B : Base
    {
        protected override string RunCore(Input input) => HexValue(Knot.Hash(input));
    }
}