namespace Advent
{
    using System;
    using System.Collections.Generic;

    public abstract class Day04 : DayBase<int>
    {
        protected static bool ValidPassphrase(Input line, bool allowAnagrams)
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