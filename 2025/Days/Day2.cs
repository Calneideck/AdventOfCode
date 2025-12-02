using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day2 : Day
    {
        string line = File.ReadAllText("Input/2.txt");

        public override object Part1()
        {
            long count = 0;

            string[] lines = line.Split(',');
            foreach (string line in lines)
            {
                long[] nums = new Regex(@"(\d+)")
                    .Matches(line).Select(r => long.Parse(r.Value))
                    .ToArray();

                for (long i = nums[0]; i <= nums[1]; i++)
                {
                    if (i.ToString().Length % 2 != 0) continue;

                    string f1 = i.ToString().Substring(0, i.ToString().Length / 2);
                    string f2 = i.ToString().Substring(i.ToString().Length / 2);

                    if (f1 == f2) count += i;
                }
            }

            return count;
        }

        public override object Part2()
        {
            long count = 0;

            string[] lines = line.Split(',');
            foreach (string line in lines)
            {
                long[] nums = new Regex(@"(\d+)")
                    .Matches(line).Select(r => long.Parse(r.Value))
                    .ToArray();

                for (long i = nums[0]; i <= nums[1]; i++)
                {
                    bool match = false;
                    string num = i.ToString();

                    for (int x = 2; x < 10; x++)
                    {
                        if (num.Length % x != 0) continue;

                        int len = num.Length / x;
                        
                        string word = num[..len];

                        for (int y = len; y < num.Length; y += len)
                        {
                            if (num.Substring(y, len) != word)
                            {
                                match = false;
                                break;
                            }

                            match = true;
                        }

                        if (match)
                        {
                            count += i;
                            break;
                        }
                    }
                }
            }

            return count;
        }
    }
}
