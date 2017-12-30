namespace Advent.Day25
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new TuringMachine(input).Run();
    }
}