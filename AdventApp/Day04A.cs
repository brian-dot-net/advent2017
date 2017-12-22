namespace Advent
{
    using System.Linq;

    public class Day04A : Day04
    {
        protected override int RunCore(Input input) => input.Lines().Select(l => ValidPassphrase(l, true)).Sum(u => u ? 1 : 0);
    }
}