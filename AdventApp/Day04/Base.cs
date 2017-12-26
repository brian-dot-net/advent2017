namespace Advent.Day04
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected static class Passphrases
        {
            public static int CountValid(Input input, bool allowAnagrams)
            {
                return input.Lines().Select(l => ValidPassphrase(l, allowAnagrams)).Sum(u => u ? 1 : 0);
            }

            private static bool ValidPassphrase(Input line, bool allowAnagrams)
            {
                HashSet<string> words = new HashSet<string>();
                foreach (Input word in line.Fields())
                {
                    if (!words.Add(Translate(word.ToString(), allowAnagrams)))
                    {
                        return false;
                    }
                }

                return true;
            }

            private static string Translate(string word, bool keepOrder)
            {
                if (keepOrder)
                {
                    return word;
                }

                char[] chars = word.ToCharArray();
                Array.Sort(chars);
                return new string(chars);
            }
        }
    }
}