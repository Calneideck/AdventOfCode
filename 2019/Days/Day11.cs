﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day11 : Day
    {
        private IEnumerable<long> codeList;

        public override string Part1()
        {
            codeList = File.ReadAllText("Input/11.txt").Split(',').Select(s => long.Parse(s));
            return GetPaint(0).Count.ToString();
        }

        public override string Part2()
        {
            var paint = GetPaint(1);

            int minX = paint.Min(key => key.Key.x);
            int maxX = paint.Max(key => key.Key.x);

            int minY = paint.Min(key => key.Key.y);
            int maxY = paint.Max(key => key.Key.y);

            Console.WriteLine();

            for (int y = maxY; y >= minY; y--)
            {
                for (int x = minX; x < maxX; x++)
                    Console.Write(paint.GetValueOrDefault((x, y)) == 0 ? " " : "█");

                Console.WriteLine();
            }

            Console.WriteLine();

            return "";
        }

        Dictionary<(int x, int y), int> GetPaint(int input)
        {
            (int x, int y) pos = (0, 0);
            Dictionary<(int x, int y), int> paint = new Dictionary<(int x, int y), int>();
            int face = 0;

            LCVM vm = new LCVM(codeList.ToArray());
            vm.AddInput(input); // First panel is black

            var result = LCVM.StopCode.None;
            bool isColour = true;

            while (result != LCVM.StopCode.Halt)
            {
                result = vm.GetCodeResult();
                if (result == LCVM.StopCode.Output)
                {
                    if (isColour)
                        paint[pos] = (int)vm.GetOutput();
                    else
                    {
                        // Dir
                        face += (int)vm.GetOutput() * 2 - 1;
                        var dir = GetDirection(face);
                        pos = (pos.x + dir.x, pos.y + dir.y);
                    }

                    isColour = !isColour;
                }
                else if (result == LCVM.StopCode.Input)
                {
                    paint.TryGetValue(pos, out int panel);
                    vm.AddInput(panel);
                }
            }

            return paint;
        }

        (int x, int y) GetDirection(int facing)
        {
            int angle = facing % 4;
            while (angle < 0)
                angle += 4;

            if (angle == 0)
                return (0, 1);
            else if (angle == 1)
                return (1, 0);
            else if (angle == 2)
                return (0, -1);
            else
                return (-1, 0);
        }
    }
}
