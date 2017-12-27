namespace Advent.Day19
{
    public class A : Base
    {
        protected override string RunCore(Input input) => new RoutingDiagram(input).Run();
    }
}