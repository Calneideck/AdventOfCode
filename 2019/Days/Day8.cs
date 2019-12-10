using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day8 : Day
    {
        private List<int[]> layers = new List<int[]>();

        public override string Part1()
        {
            int[] input = File.ReadAllText("Input/8.txt").ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();

            for (int i = 0; i < input.Length / 150; i++)
            {
                layers.Add(new int[150]);
                for (int j = 0; j < 150; j++)
                    layers.Last()[j] = input[i * 150 + j];
            }

            var min = layers.OrderBy(x => x.Count(y => y == 0)).First();
            return (min.Count(x => x == 1) * min.Count(x => x == 2)).ToString();
        }

        public override string Part2()
        {
            List<int> image = new List<int>();

            for (int i = 0; i < 150; i++)
            {
                int colour = -1;

                foreach (int[] layer in layers)
                    if (layer[i] != 2)
                    {
                        colour = layer[i];
                        break;
                    }

                image.Add(colour);
            }

            PrintImage(image.ToArray());

            return base.Part2();
        }

        void PrintImage(int[] image)
        {
            Console.WriteLine();

            for (int i = 0; i < image.Length; i += 25)
            {
                for (int j = 0; j < 24; j++)
                    Console.Write(image[i + j] == 1 ? "@" : " ");

                Console.WriteLine(image[i + 24] == 1 ? "@" : " ");
            }

            Console.WriteLine();
        }
    }
}
