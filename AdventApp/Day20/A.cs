namespace Advent.Day20
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Particles(input).Run();
    }
}