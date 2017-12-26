namespace Advent.Day04
{
    using System.Linq;

    public class A : Base
    {
        protected override int RunCore(Input input) => input.Lines().Select(l => ValidPassphrase(l, true)).Sum(u => u ? 1 : 0);
    }
}