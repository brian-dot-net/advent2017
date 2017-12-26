namespace Advent.Day04
{
    public class A : Base
    {
        protected override int RunCore(Input input) => Passphrases.CountValid(input, true);
    }
}