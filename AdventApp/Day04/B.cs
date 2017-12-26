namespace Advent.Day04
{
    using System.Linq;

    public class B : Base
    {
        protected override int RunCore(Input input) => input.Lines().Select(l => ValidPassphrase(l, false)).Sum(u => u ? 1 : 0);
    }
}