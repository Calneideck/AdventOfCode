using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day3 : Day
    {
        string[] lines = File.ReadAllLines("Input/3.txt");

        Dictionary<V2, string> map = new();

        public override object Part1()
        {
            int sum = 0;

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[y].Length; x++)
                    map.Add(new V2(x, y), lines[y][x].ToString());


            for (int y = 0; y < lines.Length; y++)
            {
                bool parsingNumber = false;
                string num = "";

                for (int x = 0; x < lines[y].Length; x++)
                {
                    bool isNum = int.TryParse(lines[y][x].ToString(), out int i);
                    if (isNum)
                    {
                        if (!parsingNumber)
                            parsingNumber = true;

                        num += i.ToString();
                    }

                    if (parsingNumber && (!isNum || x == lines[0].Length - 1))
                    {
                        int number = int.Parse(num);
                        bool symbol = false;

                        for (int j = x - num.Length - 1; j <= x; j++)
                            for (int k = y - 1; k <= y + 1; k++)
                            {
                                V2 v = new(j, k);
                                if (map.ContainsKey(v) && map[v] != "." && !int.TryParse(map[v], out int _))
                                {
                                    symbol = true;
                                    break;
                                }
                            }

                        if (symbol)
                            sum += number;

                        num = "";
                        parsingNumber = false;
                    }
                }
            }

            return sum;
        }

        public override object Part2()
        {
            Dictionary<V2, List<int>> powers = new();

            for (int y = 0; y < lines.Length; y++)
            {
                bool parsingNum = false;
                string num = "";

                for (int x = 0; x < lines[y].Length; x++)
                {
                    bool isNum = int.TryParse(lines[y][x].ToString(), out int i);
                    if (isNum)
                    {
                        if (!parsingNum)
                            parsingNum = true;

                        num += i.ToString();
                    }

                    if (parsingNum && (!isNum || x == lines[0].Length - 1))
                    {
                        int number = int.Parse(num);

                        for (int j = x - num.Length - 1; j <= x; j++)
                            for (int k = y - 1; k <= y + 1; k++)
                            {
                                V2 v = new(j, k);
                                if (map.ContainsKey(v) && map[v] == "*")
                                {
                                    if (powers.ContainsKey(v))
                                        powers[v].Add(number);
                                    else
                                        powers.Add(v, new() { number });
                                }
                            }

                        num = "";
                        parsingNum = false;
                    }
                }
            }

            return powers.Values
                .Where(x => x.Count == 2)
                .Select((a) => a.Aggregate(1, (b, c) => b * c))
                .Sum();
        }
    }
}
