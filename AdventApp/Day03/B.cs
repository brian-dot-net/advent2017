namespace Advent.Day03
{
    using System.Linq;

    public sealed class B : Base
    {
        protected override int RunCore(Input input)
        {
            int n = input.Integer();
            return new Spiral(true).FirstCell(c => c.Value > n).Value;
        }
    }
}