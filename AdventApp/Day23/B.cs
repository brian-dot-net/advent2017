namespace Advent.Day23
{
    using System;

    public class B : Base
    {
        protected override int RunCore(Input input)
        {
            // This is the assembly code transformed into straightforward loops/functions.
            // It is not generalized. :(
            int h = 0;
            for (long b = 105700; b <= 122700; b += 17)
            {
                if (!IsPrime(b))
                {
                    ++h;
                }
            }

            return h;
        }

        private static bool IsPrime(long b)
        {
            if (b % 2 == 0)
            {
                return false;
            }

            long n = (long)Math.Sqrt(b);
            for (int i = 3; i <= n; i += 2)
            {
                if (b % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}