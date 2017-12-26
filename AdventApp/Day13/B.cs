namespace Advent.Day13
{
    public class B : Base
    {
        protected override int RunCore(Input input)
        {
            Firewall fw = new Firewall(input);
            int delay = 0;
            while (true)
            {
                if (fw.TryPacket(delay))
                {
                    return delay;
                }

                ++delay;
            }
        }
    }
}