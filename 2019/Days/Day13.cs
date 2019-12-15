using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day13 : Day
    {
        private IEnumerable<long> codeList;
        private int maxX, maxY;
        private int score;

        public override string Part1()
        {
            codeList = File.ReadAllText("Input/13.txt").Split(',').Select(s => long.Parse(s));
            LCVM vm = new LCVM(codeList.ToArray());

            List<long> results = new List<long>();

            var result = LCVM.StopCode.None;
            while (result != LCVM.StopCode.Halt)
            {
                result = vm.GetCodeResult();
                if (result == LCVM.StopCode.Output)
                    results.Add(vm.GetOutput());
            }

            int blocks = 0;
            for (int i = 2; i < results.Count; i += 3)
                if (results[i] == 2)
                    blocks++;

            return blocks.ToString();
        }

        public override string Part2()
        {
            Console.WriteLine("Press a key to start Part 2:");
            Console.ReadKey();
            
            var codes = codeList.ToArray();
            codes[0] = 2;
            LCVM vm = new LCVM(codes);

            List<long> results = new List<long>();
            Dictionary<(int x, int y), int> screen = new Dictionary<(int x, int y), int>();

            bool allScreen = false;
            int ballX = 0;
            int paddleX = 0;
            int count = 0;

            var result = LCVM.StopCode.None;
            while (result != LCVM.StopCode.Halt)
            {
                result = vm.GetCodeResult();

                if (result == LCVM.StopCode.Input)
                {
                    if (!allScreen)
                    {
                        // Get Max X and Y coordinates
                        allScreen = true;
                        foreach (var item in screen)
                        {
                            maxX = Math.Max(maxX, item.Key.x);
                            maxY = Math.Max(maxY, item.Key.y);
                        }
                    }

                    if (++count == 50)
                    {
                        // Visualisation
                        DrawScreen(screen);
                        count = 0;
                    }

                    vm.AddInput(Math.Sign(ballX - paddleX));
                }
                else if (result == LCVM.StopCode.Output)
                    results.Add(vm.GetOutput());

                if (results.Count == 3)
                {
                    if (results[0] == -1 && results[1] == 0)
                        score = (int)results[2];
                    else
                    {
                        int x = (int)results[0];
                        int y = (int)results[1];
                        int val = (int)results[2];
                        if (val < 0 || val > 4)
                            throw new Exception("Value out of range!");

                        screen[(x, y)] = (int)results[2];

                        if (results[2] == 3)
                            paddleX = x;
                        else if (results[2] == 4)
                            ballX = x;
                    }

                    results.Clear();
                }
            }

            DrawScreen(screen);

            return "";
        }

        void DrawScreen(Dictionary<(int x, int y), int> screen)
        {
            Console.Clear();
            Console.WriteLine("Score: " + score);

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    screen.TryGetValue((x, y), out int val);

                    char c = val switch
                    {
                        1 => '█',
                        2 => 'X',
                        3 => '-',
                        4 => 'O',
                        _ => ' '
                    };

                    Console.Write(c);
                }

                Console.WriteLine();
            }

        }
    }
}
