using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{

    class Day15 : Day
    {
        class Lens
        {
            public string Label;
            public int Focus;
        }

        string[] lines = File.ReadAllText("Input/15.txt").Split(",");

        public override object Part1()
        {
            long count = 0;

            foreach (string line in lines)
                count += Hash(line);

            return count;
        }

        public override object Part2()
        {
            long count = 0;
            List<Lens>[] boxes = new List<Lens>[256];
            for (int i = 0; i < 256; i++)
                boxes[i] = new();

            foreach (string line in lines)
                if (line.Contains("="))
                {
                    // Add lens
                    string[] temp = line.Split("=");
                    string label = temp[0];
                    long box = Hash(label);

                    int focus = int.Parse(temp[1]);
                    Lens lens = boxes[box].Find(lens => lens.Label == label);

                    if (lens != null)
                        lens.Focus = focus;
                    else
                        boxes[box].Add(new Lens() { Label = label, Focus = focus });
                }
                else
                {
                    // Remove lens
                    string[] temp = line.Split("-");
                    string label = temp[0];
                    long box = Hash(label);

                    boxes[box] = boxes[box].Where(lens => lens.Label != label).ToList();
                }

            for (int i = 0; i < 256; i++)
                for (int l = 0; l < boxes[i].Count; l++)
                    count += (i + 1) * (l + 1) * boxes[i][l].Focus;

            return count;
        }

        static long Hash(string input)
        {
            return input.Aggregate(0, (sum, c) => (sum + (byte)c) * 17 % 256);
        }
    }
}
