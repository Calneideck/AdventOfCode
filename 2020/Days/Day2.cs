using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day2 : Day
    {
        string[] lines = File.ReadAllLines("Input/2.txt");

        public override object Part1()
        {
            int sum = 0;
            foreach (string line in lines)
            {
                var temp = line.Split(' ');
                var num = temp[0].Split('-');
                int num1 = int.Parse(num[0]);
                int num2 = int.Parse(num[1]);
                char letter = temp[1][0];
                string password = temp[2];

                int count = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    char c = password[i];
                    if (c == letter)
                        count++;
                }

                if (count >= num1 && count <= num2) sum++;
            }

            return sum.ToString();
        }

        public override object Part2()
        {
            int sum = 0;
            foreach (string line in lines)
            {
                var temp = line.Split(' ');
                var num = temp[0].Split('-');
                int num1 = int.Parse(num[0]);
                int num2 = int.Parse(num[1]);
                char letter = temp[1][0];
                string password = temp[2];

                int count = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    if (i != num1 - 1 && i != num2 - 1)
                        continue;
                    
                    char c = password[i];
                    if (c == letter)
                        count++;
                }

                if (count == 1) sum++;
            }

            return sum.ToString();
        }
    }
}
