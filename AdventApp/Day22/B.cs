namespace Advent.Day22
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new Grid(input).Run(true, 10000000);
    }
}