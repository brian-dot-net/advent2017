namespace Advent.Day23
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Coprocessor(input).Run(true).MultiplyCount;
    }
}