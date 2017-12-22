namespace Advent
{
    using System.Linq;

    public sealed class Day03A : Day03
    {
        protected override int RunCore(Input input)
        {
            int n = input.Integer();
            return new Spiral(false).FirstCell(c => c.Value == n).Distance;
        }
    }
}