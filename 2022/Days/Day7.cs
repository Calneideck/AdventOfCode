using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day7 : Day
    {
        string[] lines = File.ReadAllLines("Input/7.txt");
        Dictionary<string, long> map = new();

        public override object Part1()
        {
            Stack<string> cd = new Stack<string>();

            foreach (string line in lines)
            {
                if (line.Contains("$ cd /"))
                {
                    cd.Clear();
                    cd.Push("/");
                }
                else if (line.Contains("$ cd .."))
                {
                    cd.Pop();
                }
                else if (line.StartsWith("$ cd"))
                {
                    string newDir = line.Split(' ')[2];
                    cd.Push(newDir);
                }
                else if (int.TryParse(line[0].ToString(), out _))
                {
                    string key = "";
                    var list = cd.ToArray().Reverse();

                    foreach (var dir in list)
                    {
                        key += (key.Length > 0 ? "-" : "") + dir;
                        if (!map.ContainsKey(key))
                            map.Add(key, 0);

                        map[key] += long.Parse(line.Split(' ')[0]);
                    }
                }
            }

            return map.Where(x => x.Value <= 100000).Sum(x => x.Value);
        }

        public override object Part2()
        {
            long unused = 70000000 - map["/"];
            long req = 30000000 - unused;
            
            return map.Where(x => x.Value >= req).Min(x => x.Value);
        }
    }
}
