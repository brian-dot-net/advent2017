namespace Advent.Day05
{
    using System.Linq;

    public class B : Base
    {
        protected override int RunCore(Input input) => new JumpTable(input, o => (o > 2) ? -1 : 1).Count();
    }
}