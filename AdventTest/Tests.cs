namespace Advent.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class Tests
    {
        [TestMethod]
        public void TestShow()
        {
            StringBuilder output = new StringBuilder();
            using (TextWriter w = new StringWriter(output))
            {
                Day<ADayZ>.Show(w);
                Day<BDayX>.Show(w);
            }

            output.ToString().Should().Match(
                "ADayZ => xhello-1 (* ms elapsed)\r\n" +
                "BDayX => 500 (* ms elapsed)\r\n");
        }

        [TestMethod]
        public void TestMissingInputResource()
        {
            Action act = () => Day<BadDayX>.Run();

            act.ShouldThrow<FileNotFoundException>().WithMessage("*'BadDay.txt'*");
        }

        [TestMethod]
        public void Day01ATests()
        {
            Do<Day01A>.Tests(
                P("1122", 3),
                P("1111", 4),
                P("1234", 0),
                P("91212129", 9));
        }

        [TestMethod]
        public void Day01ASolution()
        {
            Do<Day01A>.Solution("1069");
        }

        [TestMethod]
        public void Day01BTests()
        {
            Do<Day01B>.Tests(
                P("1212", 6),
                P("1221", 0),
                P("123425", 4),
                P("123123", 12),
                P("12131415", 4));
        }

        [TestMethod]
        public void Day01BSolution()
        {
            Do<Day01B>.Solution("1268");
        }

        [TestMethod]
        public void Day02ATests()
        {
            string i1 = @"5 1 9 5
7 5 3
2 4 6 8";
            Do<Day02A>.Tests(P(i1, 18));
        }

        [TestMethod]
        public void Day02ASolution()
        {
            Do<Day02A>.Solution("51833");
        }

        [TestMethod]
        public void Day02BTests()
        {
            string i1 = @"5 9 2 8
9 4 7 3
3 8 6 5";
            Do<Day02B>.Tests(P(i1, 9));
        }

        [TestMethod]
        public void Day02BSolution()
        {
            Do<Day02B>.Solution("288");
        }

        [TestMethod]
        public void Day03ATests()
        {
            Do<Day03A>.Tests(
                P("1", 0),
                P("12", 3),
                P("23", 2),
                P("1024", 31));
        }

        [TestMethod]
        public void Day03ASolution()
        {
            Do<Day03A>.Solution("438");
        }

        [TestMethod]
        public void Day03BTests()
        {
            Do<Day03B>.Tests(
                P("1", 2),
                P("12", 23),
                P("23", 25),
                P("747", 806));
        }

        [TestMethod]
        public void Day03BSolution()
        {
            Do<Day03B>.Solution("266330");
        }

        [TestMethod]
        public void Day04ATests()
        {
            string many = @"a b c d
e f g h
a a b c
a b c c";
            Do<Day04A>.Tests(
                P("aa bb cc dd ee", 1),
                P("aa bb cc dd aa", 0),
                P("aa bb cc dd aaa", 1),
                P(many, 2));
        }

        [TestMethod]
        public void Day04ASolution()
        {
            Do<Day04A>.Solution("325");
        }

        [TestMethod]
        public void Day04BTests()
        {
            string many = @"a b c d
e f g h
a a b c
a b ca ac";
            Do<Day04B>.Tests(
                P("abcde fghij", 1),
                P("abcde xyz ecdab", 0),
                P("a ab abc abd abf abj", 1),
                P("iiii oiii ooii oooi oooo", 1),
                P("oiii ioii iioi iiio", 0),
                P(many, 2));
        }

        [TestMethod]
        public void Day04BSolution()
        {
            Do<Day04B>.Solution("119");
        }

        [TestMethod]
        public void Day05ATests()
        {
            string i1 = @"0
3
0
1
-3";
            Do<Day05A>.Tests(
                P(i1, 5));
        }

        [TestMethod]
        public void Day05ASolution()
        {
            Do<Day05A>.Solution("342669");
        }

        [TestMethod]
        public void Day05BTests()
        {
            string i1 = @"0
3
0
1
-3";
            Do<Day05B>.Tests(
                P(i1, 10));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day05BSolution()
        {
            Do<Day05B>.Solution("25136209");
        }

        [TestMethod]
        public void Day06ATests()
        {
            Do<Day06A>.Tests(
                P("0 2 7 0", 5));
        }

        [TestMethod]
        public void Day06ASolution()
        {
            Do<Day06A>.Solution("4074");
        }

        [TestMethod]
        public void Day06BTests()
        {
            Do<Day06B>.Tests(
                P("0 2 7 0", 4));
        }

        [TestMethod]
        public void Day06BSolution()
        {
            Do<Day06B>.Solution("2793");
        }


        [TestMethod]
        public void Day07ATests()
        {
            string i1 = @"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)";
            Do<Day07A>.Tests(
                P(i1, "tknk"));
        }

        [TestMethod]
        public void Day07ASolution()
        {
            Do<Day07A>.Solution("ahnofa");
        }

        [TestMethod]
        public void Day07BTests()
        {
            string i1 = @"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)";
            Do<Day07B>.Tests(
                P(i1, "ugml=60"));
        }

        [TestMethod]
        public void Day07BSolution()
        {
            Do<Day07B>.Solution("ltleg=802");
        }

        [TestMethod]
        public void Day08ATests()
        {
            string i1 = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";
            Do<Day08A>.Tests(
                P(i1, 1));
        }

        [TestMethod]
        public void Day08ASolution()
        {
            Do<Day08A>.Solution("5849");
        }

        [TestMethod]
        public void Day08BTests()
        {
            string i1 = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";
            Do<Day08B>.Tests(
                P(i1, 10));
        }

        [TestMethod]
        public void Day08BSolution()
        {
            Do<Day08B>.Solution("6702");
        }

        [TestMethod]
        public void Day09ATests()
        {
            Do<Day09A>.Tests(
                P("{}", 1),
                P("{{{}}}", 6),
                P("{{},{}}", 5),
                P("{{{},{},{{}}}}", 16),
                P("{<a>,<a>,<a>,<a>}", 1),
                P("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9),
                P("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9),
                P("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3));
        }

        [TestMethod]
        public void Day09ASolution()
        {
            Do<Day09A>.Solution("17390");
        }

        [TestMethod]
        public void Day09BTests()
        {
            Do<Day09B>.Tests(
                P("{<>}", 0),
                P("{<random characters>}", 17),
                P("{<<<<>}", 3),
                P("{<{!>}>}", 2),
                P("{<!!>}", 0),
                P("{<!!!>>}", 0),
                P("{<{o\"i!a,<{i<a>}", 10));
        }

        [TestMethod]
        public void Day09BSolution()
        {
            Do<Day09B>.Solution("7825");
        }

        [TestMethod]
        public void Day10ATests()
        {
            Do<Day10A>.Tests(
                P("5 3,4,1,5", 12));
        }

        [TestMethod]
        public void Day10ASolution()
        {
            Do<Day10A>.Solution("11413");
        }

        [TestMethod]
        public void Day10BTests()
        {
            Do<Day10B>.Tests(
                P(string.Empty, "a2582a3a0e66e6e86e3812dcb672a272"),
                P("AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd"),
                P("1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d"),
                P("1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e"));
        }

        [TestMethod]
        public void Day10BSolution()
        {
            Do<Day10B>.Solution("7adfd64c2a03a4968cf708d1b7fd418d");
        }

        [TestMethod]
        public void Day11ATests()
        {
            Do<Day11A>.Tests(
                P("ne,ne,ne", 3),
                P("ne,ne,sw,sw", 0),
                P("ne,ne,s,s", 2),
                P("se,sw,se,sw,sw", 3),
                P("n,ne,ne,ne,ne", 5),
                P("ne,se,ne,se,ne,se", 6),
                P("nw,sw,nw,sw,nw,sw", 6),
                P("n,n,n,n,se,se,se,se,se,se,se,se", 8),
                P("n,n,n,se,se,se", 3),
                P("s,s,s,s,s,ne,ne,ne", 5),
                P("n,n,n,n,n,se,se,se", 5));
        }

        [TestMethod]
        public void Day11ASolution()
        {
            Do<Day11A>.Solution("808");
        }

        [TestMethod]
        public void Day11BTests()
        {
            Do<Day11B>.Tests(
                P("ne,ne,ne", 3),
                P("ne,ne,sw,sw", 2),
                P("ne,ne,s,s", 2),
                P("se,sw,se,sw,sw", 3),
                P("n,ne,ne,ne,ne", 5),
                P("ne,se,ne,se,ne,se", 6),
                P("nw,sw,nw,sw,nw,sw", 6),
                P("n,n,n,n,se,se,se,se,se,se,se,se", 8),
                P("n,n,n,se,se,se", 3),
                P("s,s,s,s,s,ne,ne,ne", 5),
                P("n,n,n,n,n,se,se,se", 5),
                P("n,n,n,n,n,s,s,s,s,s", 5),
                P("s,s,s,s,s,n,n,n,n,n", 5),
                P("s,s,s,n,n,n,n,n,n,n,n,n", 6),
                P("n,n,n,s,s,s,s,s,s,s,s,s", 6));
        }

        [TestMethod]
        public void Day11BSolution()
        {
            Do<Day11B>.Solution("1556");
        }

        [TestMethod]
        public void Day12ATests()
        {
            string i1 = @"0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5";
            Do<Day12A>.Tests(
                P(i1, 6));
        }

        [TestMethod]
        public void Day12ASolution()
        {
            Do<Day12A>.Solution("175");
        }

        [TestMethod]
        public void Day12BTests()
        {
            string i1 = @"0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5";
            Do<Day12B>.Tests(
                P(i1, 2));
        }

        [TestMethod]
        public void Day12BSolution()
        {
            Do<Day12B>.Solution("213");
        }

        [TestMethod]
        public void Day13ATests()
        {
            string i1 = @"0: 3
1: 2
4: 4
6: 4";
            Do<Day13A>.Tests(
                P(i1, 24));
        }

        [TestMethod]
        public void Day13ASolution()
        {
            Do<Day13A>.Solution("1632");
        }

        [TestMethod]
        public void Day13BTests()
        {
            string i1 = @"0: 3
1: 2
4: 4
6: 4";
            Do<Day13B>.Tests(
                P(i1, 10));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day13BSolution()
        {
            Do<Day13B>.Solution("3834136");
        }

        [TestMethod]
        public void Day14ATests()
        {
            Do<Day14A>.Tests(
                P("flqrgnkx", 8108));
        }

        [TestMethod]
        public void Day14ASolution()
        {
            Do<Day14A>.Solution("8204");
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day14BTests()
        {
            Do<Day14B>.Tests(
                P("flqrgnkx", 1242));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day14BSolution()
        {
            Do<Day14B>.Solution("1089");
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day15ATests()
        {
            string i1 = @"Generator A starts with 65
Generator B starts with 8921";
            Do<Day15A>.Tests(
                P(i1, 588));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day15ASolution()
        {
            Do<Day15A>.Solution("612");
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day15BTests()
        {
            string i1 = @"Generator A starts with 65
Generator B starts with 8921";
            Do<Day15B>.Tests(
                P(i1, 309));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day15BSolution()
        {
            Do<Day15B>.Solution("285");
        }

        [TestMethod]
        public void Day16ATests()
        {
            Do<Day16A>.Tests(
                P("C5,s1,x3/4,pe/b", "baedc"),
                P("C6,s1", "fabcde"),
                P("C6,s2", "efabcd"));
        }

        [TestMethod]
        public void Day16ASolution()
        {
            Do<Day16A>.Solution("kbednhopmfcjilag");
        }

        private static KeyValuePair<string, string> P<TValue>(string key, TValue value)
        {
            return new KeyValuePair<string, string>(key, value.ToString());
        }

        private static class Do<TDay> where TDay : ICanRun, new()
        {
            public static void Tests(params KeyValuePair<string, string>[] pairs)
            {
                foreach (KeyValuePair<string, string> pair in pairs)
                {
                    string input = pair.Key;
                    string result = string.Empty;
                    Action act = () => result = new TDay().Run(pair.Key);

                    act.ShouldNotThrow("{0} was the input", input);
                    result.Should().Be(pair.Value, "{0} was the input", input);
                }
            }

            public static void Solution(string expected)
            {
                Day<TDay>.Run().Should().Be(expected);
            }
        }

        private sealed class ADayZ : DayBase<string>
        {
            protected override string RunCore(Input input) => "x" + input;
        }

        private sealed class BDayX : DayBase<int>
        {
            protected override int RunCore(Input input) => input.Integer();
        }

        private sealed class BadDayX : DayBase<int>
        {
            protected override int RunCore(Input input)
            {
                throw new NotImplementedException();
            }
        }
    }
}
