namespace Advent.Day15
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Generators(input).Run(40000000, 1, 1);
    }
}