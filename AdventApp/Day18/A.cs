namespace Advent.Day18
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Duet(input).Run();
    }
}