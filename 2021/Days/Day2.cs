using System.IO;

namespace AdventOfCode
{
    class Day2 : Day
    {
        string[] lines = File.ReadAllLines("Input/2.txt");

        public override object Part1()
        {
            V2 v = new V2();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp[0] == "forward") v += V2.Right * int.Parse(temp[1]);
                if (temp[0] == "up") v += V2.Up * int.Parse(temp[1]);
                if (temp[0] == "down") v += V2.Down * int.Parse(temp[1]);
            }

            return v.x * v.y;
        }

        public override object Part2()
        {
            V2 v = new V2();
            int aim = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                var temp = lines[i].Split(' ');
                if (temp[0] == "forward") {
                    v += V2.Right * int.Parse(temp[1]);
                    v += V2.Down * int.Parse(temp[1]) * aim;
                }

                if (temp[0] == "up") aim -= int.Parse(temp[1]);
                if (temp[0] == "down") aim += int.Parse(temp[1]);
            }

            return v.x * v.y;
        }
    }
}
