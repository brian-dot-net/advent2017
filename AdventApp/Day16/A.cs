namespace Advent.Day16
{
    public class A : Base
    {
        protected override string RunCore(Input input) => new Dance(input).Run(1);
    }
}