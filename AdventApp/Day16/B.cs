namespace Advent.Day16
{
    public class B : Base
    {
        protected override string RunCore(Input input) => new Dance(input).Run(1000000000);
    }
}