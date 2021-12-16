using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Node : IHeapItem<Node>
    {
        public V2 v;
        public int risk;
        public int totalRisk = int.MaxValue;
        public bool visited;
        public int heapIndex;

        public Node(V2 v, int risk)
        {
            this.v = v;
            this.risk = risk;
        }

        public int HeapIndex
        {
            get { return heapIndex; }
            set { heapIndex = value; }
        }

        public int CompareTo(Node nodeToCompare)
        {
            return nodeToCompare.totalRisk - totalRisk;
        }
    }

    class Day15 : Day
    {
        string[] lines = File.ReadAllLines("Input/15.txt");

        public override object Part1()
        {
            Dictionary<V2, Node> map = new();
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    map.Add(new V2(x, y), new Node(new V2(x, y), int.Parse(lines[y][x].ToString())));


            return FindPath(map);
        }

        public override object Part2()
        {
            Dictionary<V2, Node> map = new();
            int size = lines.Length;

            for (int x2 = 0; x2 < 5; x2++)
                for (int y2 = 0; y2 < 5; y2++)
                    for (int y = 0; y < size; y++)
                        for (int x = 0; x < size; x++)
                        {
                            int inp = int.Parse(lines[y][x].ToString());
                            int val = inp + x2 + y2;
                            if (val > 9) val -= 9;
                            V2 v = new V2(x2 * size + x, y2 * size + y);
                            map.Add(v, new Node(v, val));
                        }


            return FindPath(map);
        }

        int FindPath(Dictionary<V2, Node> map)
        {
            Heap<Node> open = new(map.Count);
            V2 target = map.Last().Value.v;

            open.Add(map[V2.Zero]);
            map[V2.Zero].totalRisk = 0;
            int visitCount = 0;

            while (open.Count > 0)
            {
                Node next = open.RemoveFirst();
                next.visited = true;
                visitCount++;

                foreach (var dir in V2.Directions)
                    if (map.TryGetValue(next.v + dir, out Node opt))
                        if (!opt.visited)
                        {
                            int thisRisk = next.totalRisk + opt.risk;
                            if (thisRisk < opt.totalRisk)
                            {
                                opt.totalRisk = thisRisk;

                                if (!open.Contains(opt))
                                    open.Add(opt);
                                else
                                    open.UpdateItem(opt);
                            }

                            if (opt.v == target)
                                return opt.totalRisk;
                        }

            }

            return -1;
        }
    }
}
