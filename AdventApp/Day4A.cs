namespace Advent
{
    using System.Linq;

    public class Day4A : Day4
    {
        protected override int RunCore(string input) => Lines.From(input).Select(l => AllUniqueWords(l)).Sum(u => u ? 1 : 0);
    }
}