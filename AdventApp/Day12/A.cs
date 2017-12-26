namespace Advent.Day12
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Pipes(input).CountPipes(0);
    }
}