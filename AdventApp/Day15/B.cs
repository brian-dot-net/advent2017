namespace Advent.Day15
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new Generators(input).Run(5000000, 4, 8);
    }
}