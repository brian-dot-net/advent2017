namespace Advent
{
    using System.Linq;

    public class Day4A : Day4
    {
        protected override int RunCore(string input) => Lines.From(input).Select(l => ValidPassphrase(l, true)).Sum(u => u ? 1 : 0);
    }
}