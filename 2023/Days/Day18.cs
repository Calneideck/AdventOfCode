using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{

    class Day18 : Day
    {
        string[] lines = File.ReadAllLines("Input/18.txt");
        HashSet<V2> map = new();

        public override object Part1()
        {
            V2 node = V2.Zero;
            map.Add(node);

            foreach (string line in lines)
            {
                string[] temp = line.Split(" ");

                for (int i = 0; i < int.Parse(temp[1]); i++)
                {
                    if (temp[0] == "R") node += V2.Right;
                    else if (temp[0] == "U") node += V2.Up;
                    else if (temp[0] == "D") node += V2.Down;
                    else if (temp[0] == "L") node += V2.Left;

                    if (!map.Contains(node)) map.Add(node);
                }
            }

            Queue<V2> open = new();
            open.Enqueue(new V2(1, 1));

            while (open.Count > 0)
            {
                node = open.Dequeue();
                map.Add(node);

                foreach (V2 dir in V2.Directions)
                {
                    V2 next = node + dir;
                    if (!open.Contains(next) && !map.Contains(next))
                        open.Enqueue(next);
                }
            }

            return map.Count;
        }


        public override object Part2()
        {
            map.Clear();

            V2 node = V2.Zero;
            map.Add(node);

            foreach (string line in lines)
            {
                string[] temp = line.Split(" ");

                string dist = temp[2][2..7];
                char dir = temp[2][7];

                int count = int.Parse(dist, System.Globalization.NumberStyles.HexNumber);

                for (int i = 0; i < count; i++)
                {
                    if (dir == '0') node += V2.Right;
                    else if (dir == '3') node += V2.Up;
                    else if (dir == '1') node += V2.Down;
                    else if (dir == '2') node += V2.Left;

                    if (!map.Contains(node)) map.Add(node);
                }
            }

            Queue<V2> open = new();
            open.Enqueue(new V2(1, 1));

            while (open.Count > 0)
            {
                node = open.Dequeue();
                map.Add(node);

                foreach (V2 dir in V2.Directions)
                    if (!open.Contains(node + dir) && !map.Contains(node + dir))
                        open.Enqueue(node + dir);
            }

            return map.Count;
        }
    }
}
