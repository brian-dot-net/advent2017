namespace Advent
{
    using System.Linq;

    public class Day04B : Day04
    {
        protected override int RunCore(string input) => Lines.From(input).Select(l => ValidPassphrase(l, false)).Sum(u => u ? 1 : 0);
    }
}