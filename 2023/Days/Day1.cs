using System.IO;

namespace AdventOfCode
{
    class Day1 : Day
    {
        string[] lines = File.ReadAllLines("Input/1.txt");

        public override object Part1()
        {
            int sum = 0;

            foreach (var line in lines)
            {
                int? num1 = null;
                int? num2 = null;

                for (int x = 0; x < line.Length; x++)
                    if (int.TryParse(line[x].ToString(), out int num))
                        if (num1 == null)
                            num1 = num;
                        else
                            num2 = num;

                sum += int.Parse(num1.ToString() + (num2 != null ? num2.ToString() : num1.ToString()));
            }

            return sum;
        }

        public override object Part2()
        {
            int sum = 0;
            string[] words = new string[] {
                "one","two","three","four","five","six","seven","eight","nine"
            };

            foreach (var line in lines)
            {
                int? num1 = null;
                int? num2 = null;
                int index1 = -1;
                int index2 = -1;

                for (int x = 0; x < line.Length; x++)
                    if (int.TryParse(line[x].ToString(), out int num))
                        if (num1 == null)
                        {
                            num1 = num;
                            num2 = num;
                            index1 = x;
                            index2 = x;
                        }
                        else
                        {
                            num2 = num;
                            index2 = x;
                        }

                for (int x = 0; x < line.Length; x++)
                    foreach (string w in words)
                        if (line[x..].StartsWith(w))
                        {
                            if (x < index1 || index1 == -1)
                            {
                                num1 = GetDigit(w);
                                index1 = x;
                            }
                            if (x > index2)
                                num2 = GetDigit(w);
                        }

                sum += int.Parse(num1.ToString() + num2.ToString());
            }

            return sum;
        }

        static int GetDigit(string input)
        {
            return input switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                _ => 0,
            };
        }
    }
}
