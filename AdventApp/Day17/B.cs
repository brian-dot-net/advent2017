namespace Advent.Day17
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new Spinlock(input).Run(50000000, 1);
    }
}