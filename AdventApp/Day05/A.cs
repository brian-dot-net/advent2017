namespace Advent.Day05
{
    using System.Linq;

    public class A : Base
    {
        protected override int RunCore(Input input) => new JumpTable(input, o => 1).Count();
    }
}