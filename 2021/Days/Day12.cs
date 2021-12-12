using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day12 : Day
    {
        class Cave
        {
            public string n;
            public bool big;
            public List<Cave> links;

            public Cave(string name)
            {
                n = name;
                big = name.ToUpper() == name;
                links = new();
            }

            public void AddLink(Cave c)
            {
                links.Add(c);
            }
        }


        string[] lines = File.ReadAllLines("Input/12.txt");
        HashSet<string> paths = new();
        Dictionary<string, Cave> caves = new();


        public override object Part1()
        {
            foreach (var line in lines)
            {
                string[] temp = line.Split("-");
                caves.TryAdd(temp[0], new Cave(temp[0]));
                caves.TryAdd(temp[1], new Cave(temp[1]));

                caves[temp[0]].AddLink(caves[temp[1]]);

                if (temp[0] != "start")
                    caves[temp[1]].AddLink(caves[temp[0]]);
            }

            FindNext(caves["start"], "");

            return paths.Count;
        }

        void FindNext(Cave c, string route)
        {
            if (c.n == "end")
            {
                if (!paths.Contains(route))
                    paths.Add(route);

                return; 
            }

            foreach (Cave cave in c.links)
                if (cave.big || !route.Split(',').Contains(cave.n))
                    FindNext(cave, route + "," + cave.n);
        }


        public override object Part2()
        {
            paths.Clear();
            FindNext2(caves["start"], "");
            return paths.Count;
        }

        void FindNext2(Cave c, string route)
        {
            if (c.n == "end")
            {
                if (!paths.Contains(route))
                    paths.Add(route);

                return;
            }

            foreach (Cave cave in c.links)
                if (cave.big || CheckDistincts(route) || !route.Split(',').Contains(cave.n))
                    FindNext2(cave, route + "," + cave.n);
        }

        bool CheckDistincts(string route)
        {
            string[] temp = route.Split(',');
            foreach (var c in temp)
                if (c.Length > 0 && c.ToLower() == c)
                    if (temp.Count(x => x == c) >= 2)
                        return false;

            return true;
        }
    }
}
