namespace Advent.Day21
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Art(input).Run(5);
    }
}