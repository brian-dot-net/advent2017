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
        private static readonly string NL = Environment.NewLine;

        [TestMethod]
        public void TestShow()
        {
            StringBuilder output = new StringBuilder();
            using (TextWriter w = new StringWriter(output))
            {
                Day<DayA.A>.Show(w);
                Day<DayZZ.B>.Show(w);
            }

            output.ToString().Should().Match(
                "DayA.A => xhello-1 (* ms elapsed)\r\n" +
                "DayZZ.B => 500 (* ms elapsed)\r\n");
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
            Do<Day01.A>.Tests(
                P("1122", 3),
                P("1111", 4),
                P("1234", 0),
                P("91212129", 9));
        }

        [TestMethod]
        public void Day01ASolution()
        {
            Do<Day01.A>.Solution("1069");
        }

        [TestMethod]
        public void Day01BTests()
        {
            Do<Day01.B>.Tests(
                P("1212", 6),
                P("1221", 0),
                P("123425", 4),
                P("123123", 12),
                P("12131415", 4));
        }

        [TestMethod]
        public void Day01BSolution()
        {
            Do<Day01.B>.Solution("1268");
        }

        [TestMethod]
        public void Day02ATests()
        {
            string i1 = @"5 1 9 5
7 5 3
2 4 6 8";
            Do<Day02.A>.Tests(P(i1, 18));
        }

        [TestMethod]
        public void Day02ASolution()
        {
            Do<Day02.A>.Solution("51833");
        }

        [TestMethod]
        public void Day02BTests()
        {
            string i1 = @"5 9 2 8
9 4 7 3
3 8 6 5";
            Do<Day02.B>.Tests(P(i1, 9));
        }

        [TestMethod]
        public void Day02BSolution()
        {
            Do<Day02.B>.Solution("288");
        }

        [TestMethod]
        public void Day03ATests()
        {
            Do<Day03.A>.Tests(
                P("1", 0),
                P("12", 3),
                P("23", 2),
                P("1024", 31));
        }

        [TestMethod]
        public void Day03ASolution()
        {
            Do<Day03.A>.Solution("438");
        }

        [TestMethod]
        public void Day03BTests()
        {
            Do<Day03.B>.Tests(
                P("1", 2),
                P("12", 23),
                P("23", 25),
                P("747", 806));
        }

        [TestMethod]
        public void Day03BSolution()
        {
            Do<Day03.B>.Solution("266330");
        }

        [TestMethod]
        public void Day04ATests()
        {
            string many = @"a b c d
e f g h
a a b c
a b c c";
            Do<Day04.A>.Tests(
                P("aa bb cc dd ee", 1),
                P("aa bb cc dd aa", 0),
                P("aa bb cc dd aaa", 1),
                P(many, 2));
        }

        [TestMethod]
        public void Day04ASolution()
        {
            Do<Day04.A>.Solution("325");
        }

        [TestMethod]
        public void Day04BTests()
        {
            string many = @"a b c d
e f g h
a a b c
a b ca ac";
            Do<Day04.B>.Tests(
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
            Do<Day04.B>.Solution("119");
        }

        [TestMethod]
        public void Day05ATests()
        {
            string i1 = @"0
3
0
1
-3";
            Do<Day05.A>.Tests(
                P(i1, 5));
        }

        [TestMethod]
        public void Day05ASolution()
        {
            Do<Day05.A>.Solution("342669");
        }

        [TestMethod]
        public void Day05BTests()
        {
            string i1 = @"0
3
0
1
-3";
            Do<Day05.B>.Tests(
                P(i1, 10));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day05BSolution()
        {
            Do<Day05.B>.Solution("25136209");
        }

        [TestMethod]
        public void Day06ATests()
        {
            Do<Day06.A>.Tests(
                P("0 2 7 0", 5));
        }

        [TestMethod]
        public void Day06ASolution()
        {
            Do<Day06.A>.Solution("4074");
        }

        [TestMethod]
        public void Day06BTests()
        {
            Do<Day06.B>.Tests(
                P("0 2 7 0", 4));
        }

        [TestMethod]
        public void Day06BSolution()
        {
            Do<Day06.B>.Solution("2793");
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
            Do<Day07.A>.Tests(
                P(i1, "tknk"));
        }

        [TestMethod]
        public void Day07ASolution()
        {
            Do<Day07.A>.Solution("ahnofa");
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
            Do<Day07.B>.Tests(
                P(i1, "ugml=60"));
        }

        [TestMethod]
        public void Day07BSolution()
        {
            Do<Day07.B>.Solution("ltleg=802");
        }

        [TestMethod]
        public void Day08ATests()
        {
            string i1 = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";
            Do<Day08.A>.Tests(
                P(i1, 1));
        }

        [TestMethod]
        public void Day08ASolution()
        {
            Do<Day08.A>.Solution("5849");
        }

        [TestMethod]
        public void Day08BTests()
        {
            string i1 = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";
            Do<Day08.B>.Tests(
                P(i1, 10));
        }

        [TestMethod]
        public void Day08BSolution()
        {
            Do<Day08.B>.Solution("6702");
        }

        [TestMethod]
        public void Day09ATests()
        {
            Do<Day09.A>.Tests(
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
            Do<Day09.A>.Solution("17390");
        }

        [TestMethod]
        public void Day09BTests()
        {
            Do<Day09.B>.Tests(
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
            Do<Day09.B>.Solution("7825");
        }

        [TestMethod]
        public void Day10ATests()
        {
            Do<Day10.A>.Tests(
                P("5 3,4,1,5", 12));
        }

        [TestMethod]
        public void Day10ASolution()
        {
            Do<Day10.A>.Solution("11413");
        }

        [TestMethod]
        public void Day10BTests()
        {
            Do<Day10.B>.Tests(
                P(string.Empty, "a2582a3a0e66e6e86e3812dcb672a272"),
                P("AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd"),
                P("1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d"),
                P("1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e"));
        }

        [TestMethod]
        public void Day10BSolution()
        {
            Do<Day10.B>.Solution("7adfd64c2a03a4968cf708d1b7fd418d");
        }

        [TestMethod]
        public void Day11ATests()
        {
            Do<Day11.A>.Tests(
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
            Do<Day11.A>.Solution("808");
        }

        [TestMethod]
        public void Day11BTests()
        {
            Do<Day11.B>.Tests(
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
            Do<Day11.B>.Solution("1556");
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
            Do<Day12.A>.Tests(
                P(i1, 6));
        }

        [TestMethod]
        public void Day12ASolution()
        {
            Do<Day12.A>.Solution("175");
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
            Do<Day12.B>.Tests(
                P(i1, 2));
        }

        [TestMethod]
        public void Day12BSolution()
        {
            Do<Day12.B>.Solution("213");
        }

        [TestMethod]
        public void Day13ATests()
        {
            string i1 = @"0: 3
1: 2
4: 4
6: 4";
            Do<Day13.A>.Tests(
                P(i1, 24));
        }

        [TestMethod]
        public void Day13ASolution()
        {
            Do<Day13.A>.Solution("1632");
        }

        [TestMethod]
        public void Day13BTests()
        {
            string i1 = @"0: 3
1: 2
4: 4
6: 4";
            Do<Day13.B>.Tests(
                P(i1, 10));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day13BSolution()
        {
            Do<Day13.B>.Solution("3834136");
        }

        [TestMethod]
        public void Day14ATests()
        {
            Do<Day14.A>.Tests(
                P("flqrgnkx", 8108));
        }

        [TestMethod]
        public void Day14ASolution()
        {
            Do<Day14.A>.Solution("8204");
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day14BTests()
        {
            Do<Day14.B>.Tests(
                P("flqrgnkx", 1242));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day14BSolution()
        {
            Do<Day14.B>.Solution("1089");
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day15ATests()
        {
            string i1 = @"Generator A starts with 65
Generator B starts with 8921";
            Do<Day15.A>.Tests(
                P(i1, 588));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day15ASolution()
        {
            Do<Day15.A>.Solution("612");
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day15BTests()
        {
            string i1 = @"Generator A starts with 65
Generator B starts with 8921";
            Do<Day15.B>.Tests(
                P(i1, 309));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day15BSolution()
        {
            Do<Day15.B>.Solution("285");
        }

        [TestMethod]
        public void Day16ATests()
        {
            Do<Day16.A>.Tests(
                P("C5,s1,x3/4,pe/b", "baedc"),
                P("C6,s1", "fabcde"),
                P("C6,s2", "efabcd"));
        }

        [TestMethod]
        public void Day16ASolution()
        {
            Do<Day16.A>.Solution("kbednhopmfcjilag");
        }

        [TestMethod]
        public void Day16BTests()
        {
            Do<Day16.B>.Tests(
                P("C5,x0/1", "abcde"));
        }

        [TestMethod]
        public void Day16BSolution()
        {
            Do<Day16.B>.Solution("fbmcgdnjakpioelh");
        }

        [TestMethod]
        public void Day17ATests()
        {
            Do<Day17.A>.Tests(
                P("3", 638));
        }

        [TestMethod]
        public void Day17ASolution()
        {
            Do<Day17.A>.Solution("1282");
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day17BTests()
        {
            Do<Day17.B>.Tests(
                P("0", 1));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day17BSolution()
        {
            Do<Day17.B>.Solution("27650600");
        }

        [TestMethod]
        public void Day18ATests()
        {
            string i1 = @"set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2";
            Do<Day18.A>.Tests(
                P(i1, 4));
        }

        [TestMethod]
        public void Day18ASolution()
        {
            Do<Day18.A>.Solution("4601");
        }

        [TestMethod]
        public void Day18BTests()
        {
            string i1 = @"snd 1
snd 2
snd p
rcv a
rcv b
rcv c
rcv d";
            Do<Day18.B>.Tests(
                P(i1, 3));
        }

        [TestMethod]
        public void Day18BSolution()
        {
            Do<Day18.B>.Solution("6858");
        }

        [TestMethod]
        public void Day19ATests()
        {
            string i1 =
                "     |          " + NL +
                "     |  +--+    " + NL +
                "     A  |  C    " + NL +
                " F---|----E|--+ " + NL +
                "     |  |  |  D " + NL +
                "     +B-+  +--+ ";
            Do<Day19.A>.Tests(
                P(i1, "ABCDEF"));
        }

        [TestMethod]
        public void Day19ASolution()
        {
            Do<Day19.A>.Solution("MOABEUCWQS");
        }

        [TestMethod]
        public void Day19BTests()
        {
            string i1 = 
                "     |          " + NL +
                "     |  +--+    " + NL +
                "     A  |  C    " + NL +
                " F---|----E|--+ " + NL +
                "     |  |  |  D " + NL +
                "     +B-+  +--+ ";
            Do<Day19.B>.Tests(
                P(i1, "38"));
        }

        [TestMethod]
        public void Day19BSolution()
        {
            Do<Day19.B>.Solution("18058");
        }

        [TestMethod]
        public void Day20ATests()
        {
            string i1 =
@"p=<3,0,0>, v=<2,0,0>, a=<-1,0,0>
p=<4,0,0>, v=<0,0,0>, a=<-2,0,0>";
            Do<Day20.A>.Tests(
                P(i1, 0));
        }

        [TestMethod]
        public void Day20ASolution()
        {
            Do<Day20.A>.Solution("144");
        }

        [TestMethod]
        public void Day20BTests()
        {
            string i1 =
@"p=<-6,0,0>, v=<3,0,0>, a=<0,0,0>
p=<-4,0,0>, v=<2,0,0>, a=<0,0,0>
p=<-2,0,0>, v=<1,0,0>, a=<0,0,0>
p=<3,0,0>, v=<-1,0,0>, a=<0,0,0>";
            Do<Day20.B>.Tests(
                P(i1, 1));
        }

        [TestMethod]
        public void Day20BSolution()
        {
            Do<Day20.B>.Solution("477");
        }

        [TestMethod]
        public void Day21ATests()
        {
            string i1 = @"N => 2
../.# => ##./#../...
.#./..#/### => #..#/..../..../#..#";
            Do<Day21.A>.Tests(
                P(i1, 12));
        }

        [TestMethod]
        public void Day21ASolution()
        {
            Do<Day21.A>.Solution("179");
        }

        [TestMethod]
        public void Day21BTests()
        {
            string i1 = @"N => 2
../.# => ##./#../...
.#./..#/### => #..#/..../..../#..#";
            Do<Day21.B>.Tests(
                P(i1, 12));
        }

        [TestMethod]
        public void Day21BSolution()
        {
            Do<Day21.B>.Solution("2766750");
        }

        [TestMethod]
        public void Day22ATests()
        {
            string i1 =
                "..#" + NL +
                "#.." + NL +
                "...";
            Do<Day22.A>.Tests(
                P(i1, 5587));
        }

        [TestMethod]
        public void Day22ASolution()
        {
            Do<Day22.A>.Solution("5348");
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day22BTests()
        {
            string i1 =
                "................." + NL +
                "................." + NL +
                "................." + NL +
                "................." + NL +
                "................." + NL +
                "................." + NL +
                "................." + NL +
                ".........#......." + NL +
                ".......#........." + NL +
                "................." + NL +
                "................." + NL +
                "................." + NL +
                "................." + NL +
                "................." + NL +
                "................." + NL +
                "................." + NL +
                ".................";
            Do<Day22.B>.Tests(
                P(i1, 2511944));
        }

        [TestMethod]
        [TestCategory("Slow")]
        public void Day22BSolution()
        {
            Do<Day22.B>.Solution("2512225");
        }

        [TestMethod]
        public void Day23ATests()
        {
            string i1 = @"set a 1
set b 1
sub a b
jnz a 3
mul c 1
mul b 2
jnz b 1";
            Do<Day23.A>.Tests(
                P(i1, 2));
        }

        [TestMethod]
        public void Day23ASolution()
        {
            Do<Day23.A>.Solution("3025");
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

        private sealed class BadDayX : DayBase<int>
        {
            protected override int RunCore(Input input)
            {
                throw new NotImplementedException();
            }
        }
    }
}
