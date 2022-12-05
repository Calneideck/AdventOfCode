using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day5 : Day
    {
        string[] lines = File.ReadAllLines("Input/5.txt");

        public override object Part1()
        {
            Stack<char>[] stacks = null;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(" 1   2   3"))
                {
                    int length = (lines[i].Length + 1) / 4;
                    stacks = new Stack<char>[length];
                    for (int j = 0; j < length; j++)
                        stacks[j] = new Stack<char>();

                    for (int j = i - 1; j >= 0; j--)
                    {
                        int index = 0;
                        for (int x = 1; x < lines[j].Length; x += 4)
                        {
                            if (lines[j][x] != ' ')
                                stacks[index].Push(lines[j][x]);

                            index++;
                        }
                    }
                }

                if (lines[i].Contains("move"))
                {
                    int[] moves = new Regex(@"(\d+)").Matches(lines[i]).Select(x => int.Parse(x.Value)).ToArray();
                    Stack<char> tempStack = new();

                    for (int j = 0; j < moves[0]; j++)
                        stacks[moves[2] - 1].Push(stacks[moves[1] - 1].Pop());
                }
            }

            string result = "";
            foreach (var s in stacks)
                result += s.Peek();

            return result;
        }

        public override object Part2()
        {
            Stack<char>[] stacks = new Stack<char>[0];

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(" 1   2   3"))
                {
                    int length = (lines[i].Length + 1) / 4;
                    stacks = new Stack<char>[length];
                    for (int j = 0; j < length; j++)
                        stacks[j] = new Stack<char>();

                    for (int j = i - 1; j >= 0; j--)
                    {
                        int index = 0;
                        for (int x = 1; x < lines[j].Length; x += 4)
                        {
                            if (lines[j][x] != ' ')
                                stacks[index].Push(lines[j][x]);

                            index++;
                        }
                    }
                }

                if (lines[i].Contains("move"))
                {
                    int[] moves = new Regex(@"(\d+)").Matches(lines[i]).Select(x => int.Parse(x.Value)).ToArray();
                    Stack<char> tempStack = new Stack<char>();

                    for (int j = 0; j < moves[0]; j++)
                        tempStack.Push(stacks[moves[1] - 1].Pop());

                    for (int j = 0; j < moves[0]; j++)
                        stacks[moves[2] - 1].Push(tempStack.Pop());
                }
            }

            string result = "";
            foreach (var s in stacks)
                result += s.Peek();

            return result;
        }
    }
}
