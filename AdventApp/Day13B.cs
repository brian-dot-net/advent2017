namespace Advent
{
    public class Day13B : Day13
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