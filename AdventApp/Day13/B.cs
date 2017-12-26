namespace Advent.Day13
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new Firewall(input).FindSendDelay();
    }
}