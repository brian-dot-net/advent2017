namespace Advent.Day13
{
    public class A : Base
    {
        protected override int RunCore(Input input) => new Firewall(input).SendPacket();
    }
}