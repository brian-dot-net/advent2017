namespace Advent.Day19
{
    public class B : Base
    {
        protected override string RunCore(Input input)
        {
            RoutingDiagram route = new RoutingDiagram(input);
            route.Run();
            return route.Steps.ToString();
        }
    }
}