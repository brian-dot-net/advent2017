namespace Advent
{
    using System.Linq;

    public class Day04B : Day04
    {
        protected override int RunCore(Input input) => input.Lines().Select(l => ValidPassphrase(l, false)).Sum(u => u ? 1 : 0);
    }
}