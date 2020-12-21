using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day18 : Day
    {
        string[] lines = File.ReadAllLines("Input/18.txt").ToArray();

        public override object Part1()
        {
            long sum = 0;
            Stack<long> sums = new Stack<long>();
            Stack<char> ops = new Stack<char>();
            foreach (var line in lines)
            {
                long currentSum = 0;
                char currentOp = ' ';

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == ' ')
                        continue;
                    else if (line[i] == '+')
                        currentOp = '+';
                    else if (line[i] == '*')
                        currentOp = '*';
                    else if (line[i] == '(')
                    {
                        sums.Push(currentSum);
                        ops.Push(currentOp);
                        currentSum = 0;
                        currentOp = ' ';
                    }
                    else if (line[i] == ')')
                    {
                        currentOp = ops.Pop();
                        if (currentOp == ' ')
                            sums.Pop();
                        else if (currentOp == '+')
                            currentSum += sums.Pop();
                        else if (currentOp == '*')
                            currentSum *= sums.Pop();

                        currentOp = ' ';
                    }
                    else if (currentOp == '*')
                    {
                        currentSum *= int.Parse(line[i].ToString());
                        currentOp = ' ';
                    }
                    else if (currentOp == '+')
                    {
                        currentSum += int.Parse(line[i].ToString());
                        currentOp = ' ';
                    }
                    else
                        currentSum = int.Parse(line[i].ToString());
                }

                sum += currentSum;
            }

            return sum;
        }

        public override object Part2()
        {
            for (int i = 0; i < lines.Length; i++)
                for (int x = 0; x < lines[i].Length; x++)
                    if (lines[i][x] == '+')
                    {
                        int y = x - 1;
                        int slashes = 0;
                        while (true)
                        {
                            y--;
                            if (int.TryParse(lines[i][y].ToString(), out _))
                            {
                                if (slashes == 0)
                                    break;
                            }
                            else if (lines[i][y] == ')')
                                slashes++;
                            else if (lines[i][y] == '(' && --slashes == 0)
                                break;
                        }

                        lines[i] = lines[i].Insert(y, "(");
                        y = x + 1;

                        while (true)
                        {
                            y++;
                            if (int.TryParse(lines[i][y].ToString(), out _))
                            {
                                if (slashes == 0)
                                    break;
                            }
                            else if (lines[i][y] == '(')
                                slashes++;
                            else if (lines[i][y] == ')' && --slashes == 0)
                                break;
                        }

                        lines[i] = lines[i].Insert(y + 1, ")");
                        x += 4;
                    }

            return Part1();
        }
    }
}
