namespace Advent
{
    using System;
    using System.Collections.Generic;

    public abstract class Day04 : DayBase<int>
    {
        public override string DefaultInput => null;

        protected static bool ValidPassphrase(string line, bool allowAnagrams)
        {
            HashSet<string> words = new HashSet<string>();
            foreach (string word in line.Split())
            {
                if (!words.Add(Translate(word, allowAnagrams)))
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