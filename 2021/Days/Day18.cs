using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day18 : Day
    {
        string[] lines = File.ReadAllLines("Input/18.txt");

        public override object Part1()
        {
            string line = lines[0];
            for (int i = 1; i < lines.Length; i++)
            {
                string newLine = "[" + line + "," + lines[i] + "]";
                bool reducing = true;

                while (reducing)
                {
                    reducing = false;
                    int openCount = 0;

                    // Explode
                    for (int x = 0; x < newLine.Length; x++)
                    {
                        if (newLine[x] == '[')
                            openCount++;
                        else if (newLine[x] == ']')
                            openCount--;
                        else if (int.TryParse(newLine[x].ToString(), out int num))
                        {
                            if (openCount > 4)
                            {
                                var match = new Regex(@"(\d+),(\d+)").Matches(newLine)
                                    .Where(m => m.Index == x).ToArray();
                                if (match.Length > 0)
                                {
                                    num = int.Parse(match[0].Groups[1].Value);
                                    int pair = int.Parse(match[0].Groups[2].Value);

                                    var matches = new Regex(@"(\d+)").Matches(newLine);
                                    int index = matches.ToList().FindIndex(m => m.Index == x);

                                    if (index > 0)
                                    {
                                        Match leftMatch = matches[index - 1];
                                        int leftNum = int.Parse(leftMatch.Value) + num;
                                        newLine = newLine.Substring(0, leftMatch.Index) + leftNum + newLine.Substring(leftMatch.Index + leftNum.ToString().Length);
                                        x += leftNum.ToString().Length - 1;
                                    }

                                    if (index < matches.Count - 2)
                                    {
                                        Match rightMatch = matches[index + 2];
                                        int rightNum = int.Parse(rightMatch.Value) + pair;
                                        newLine = newLine.Substring(0, rightMatch.Index) + rightNum + newLine.Substring(rightMatch.Index + rightNum.ToString().Length);
                                    }

                                    newLine = newLine.Substring(0, x - 1) + "0" + newLine.Substring(x + 4);
                                    reducing = true;
                                    break;
                                }
                            }

                            x += num.ToString().Length - 1;
                        }
                    }

                    if (!reducing)
                    {
                        // Split
                        var matches = new Regex(@"(\d+)").Matches(newLine);
                        foreach (Match match in matches)
                            if (int.TryParse(match.Value, out int high) && high >= 10)
                            {
                                newLine = newLine.Substring(0, match.Index) + "[" + Math.Floor(high * 0.5f) + "," + Math.Ceiling(high * 0.5f) + "]" +
                                    newLine.Substring(match.Index + match.Value.Length);

                                reducing = true;
                                break;
                            }
                    }
                }

                Console.WriteLine(newLine);
            }

            return "";
        }

        public override object Part2()
        {
            return "";
        }
    }
}

/*
[
[
[
[
4
,
3
]
,
4
]
,
4
]
,
[
7
,
[
[
8
,
4
]
,
9
]
]
]
*/