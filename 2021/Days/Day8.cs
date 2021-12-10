using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day8 : Day
    {
        string[] lines = File.ReadAllLines("Input/8.txt");

        public override object Part1()
        {
            int count = 0;
            
            foreach (var line in lines)
            {
                var counts = line.Split(" | ")[1].Split(" ");
                foreach (var c in counts)
                    if (c.Length == 2 || c.Length == 4 || c.Length == 3 | c.Length == 7)
                        count++;
            }

            return count;
        }


        public override object Part2()
        {
            int sum = 0;
            
            foreach (var line in lines)
            {
                var signals = line.Split(" | ")[0].Split(" ");
                var counts = line.Split(" | ")[1].Split(" ");

                var one = signals.First(x => x.Length == 2);
                var four = signals.First(x => x.Length == 4);
                var seven = signals.First(x => x.Length == 3);

                char top = seven.Except(one).First();

                var midAndLeftT = four.Except(one);

                var temp = signals.Where(x => x.Length == 5).ToArray();
                char mid = temp[0].Intersect(temp[1]).Intersect(temp[2]).Intersect(midAndLeftT).First();

                char leftT = midAndLeftT.Where(x => x != mid).First();

                var five = signals.First(x => x.Length == 5 && x.Contains(leftT));

                char rightB = five.Intersect(one).First();

                char rightT = one.Where(x => x != rightB).First();

                for (int i = 0; i < 4; i++)
                {
                    string s = counts[i];
                    int digit = 0;

                    switch (s.Length)
                    {
                        case 2:
                            digit = 1;
                            break;

                        case 3:
                            digit = 7;
                            break;

                        case 4:
                            digit = 4;
                            break;

                        case 5:
                            if (s.Contains(leftT)) digit = 5;
                            else if (s.Contains(rightT) && s.Contains(rightB)) digit = 3;
                            else digit = 2;
                            break;

                        case 6:
                            if (!s.Contains(mid)) digit = 0;
                            else if (!s.Contains(rightT)) digit = 6;
                            else digit = 9;
                            break;

                        case 7:
                            digit = 8;
                            break;
                    }

                    sum += (int)Math.Pow(10, 3 - i) * digit;
                }
            }

            return sum;
        }
    }
}

//1 - 2 *

//7 - 3 *

//4 - 4 *

//2 - 5
//3 - 5
//5 - 5

//0 - 6
//6 - 6
//9 - 6

//8 - 7 *
