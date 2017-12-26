namespace Advent.Day04
{
    public class B : Base
    {
        protected override int RunCore(Input input) => Passphrases.CountValid(input, false);
    }
}