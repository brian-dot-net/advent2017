namespace Advent
{
    using System.Linq;

    public sealed class Day03B : Day03
    {
        protected override int RunCore(Input input)
        {
            int n = input.Integer();
            return new Spiral(true).FirstCell(c => c.Value > n).Value;
        }
    }
}