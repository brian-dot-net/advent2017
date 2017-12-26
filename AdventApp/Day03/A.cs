namespace Advent.Day03
{
    using System.Linq;

    public sealed class A : Base
    {
        protected override int RunCore(Input input)
        {
            int n = input.Integer();
            return new Spiral(false).FirstCell(c => c.Value == n).Distance;
        }
    }
}