using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day12 : Day
    {
        string[] lines = File.ReadAllLines("Input/12.txt");

        public override object Part1()
        {
            V2 pos = V2.Zero;
            V2 dir = V2.Right;

            foreach (var line in lines)
            {
                char action = line[0];
                int amount = int.Parse(line.Substring(1));
                double rad = 0;

                switch (action)
                {
                    case 'F':
                        pos += dir * amount;
                        break;

                    case 'N':
                        pos += V2.Up * amount;
                        break;

                    case 'S':
                        pos += V2.Down * amount;
                        break;

                    case 'E':
                        pos += V2.Right * amount;
                        break;

                    case 'W':
                        pos += V2.Left * amount;
                        break;

                    case 'L':
                        rad = -amount * Math.PI / 180;
                        dir = new V2(dir.x * (int)Math.Cos(rad) - dir.y * (int)Math.Sin(rad),
                            dir.x * (int)Math.Sin(rad) + dir.y * (int)Math.Cos(rad));
                        break;

                    case 'R':
                        rad = amount * Math.PI / 180;
                        dir = new V2(dir.x * (int)Math.Cos(rad) - dir.y * (int)Math.Sin(rad),
                            dir.x * (int)Math.Sin(rad) + dir.y * (int)Math.Cos(rad));
                        break;


                    default:
                        throw new Exception("Unhandled char" + line);
                }
            }

            return Math.Abs(pos.x) + Math.Abs(pos.y);
        }


        public override object Part2()
        {
            V2 pos = V2.Zero;
            V2 waypoint = new V2(10, -1);

            foreach (var line in lines)
            {
                char action = line[0];
                int amount = int.Parse(line.Substring(1));
                double rad = 0;

                switch (action)
                {
                    case 'F':
                        pos += waypoint * amount;
                        break;

                    case 'N':
                        waypoint += V2.Up * amount;
                        break;

                    case 'S':
                        waypoint += V2.Down * amount;
                        break;

                    case 'E':
                        waypoint += V2.Right * amount;
                        break;

                    case 'W':
                        waypoint += V2.Left * amount;
                        break;

                    case 'L':
                        rad = -amount * Math.PI / 180;
                        waypoint = new V2(waypoint.x * (int)Math.Cos(rad) - waypoint.y * (int)Math.Sin(rad),
                            waypoint.x * (int)Math.Sin(rad) + waypoint.y * (int)Math.Cos(rad));
                        break;

                    case 'R':
                        rad = amount * Math.PI / 180;
                        waypoint = new V2(waypoint.x * (int)Math.Cos(rad) - waypoint.y * (int)Math.Sin(rad),
                            waypoint.x * (int)Math.Sin(rad) + waypoint.y * (int)Math.Cos(rad));
                        break;


                    default:
                        throw new Exception("Unhandled char" + line);
                }
            }

            return Math.Abs(pos.x) + Math.Abs(pos.y);
        }

    }
}
