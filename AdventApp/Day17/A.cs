namespace Advent.Day17
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Spinlock(input).Run(2017);
    }
}