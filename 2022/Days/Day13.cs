using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day13 : Day
    {
        string[][] lines = File.ReadAllText("Input/13.txt").Split("\n\n").Select(x => x.Split('\n')).ToArray();

        string GetSubString(string line, int start)
        {
            if (line.Substring(start) == "[]") return "";

            int c = 0;

            for (int i = start; i < line.Length; i++)
                if (line[i] == '[')
                    c++;
                else if (line[i] == ']' && --c == 0)
                    return line.Substring(start + 1, i - start - 1);

            return null;
        }

        bool CompareList(string a, string b)
        {
            int iA = 0;
            int iB = 0;

            while (true)
            {
                if (int.TryParse(a[iA].ToString(), out int num1) && int.TryParse(b[iB].ToString(), out int num2))
                {
                    if (num1 > num2)
                        return false;
                }
                else if (a[iA] == '[' && b[iB] == '[')
                {
                    string newA = GetSubString(a, iA);
                    string newB = GetSubString(b, iB);
                    iA += newA.Length + 1;
                    iB += newB.Length + 1;

                    if (!CompareList(newA, newB)) return false;
                }
                else if (int.TryParse(a[iA].ToString(), out int _num1))
                {
                    string newB = GetSubString(b, iB);
                    iB += newB.Length + 1;

                    if (!CompareList(a[iA].ToString(), newB)) return false;
                }
                else if (int.TryParse(b[iB].ToString(), out int _num2))
                {
                    string newA = GetSubString(a, iA);
                    iA += newA.Length + 1;

                    if (!CompareList(newA, b[iB].ToString())) return false;
                }

                iA++;
                iB++;

                if (iB >= b.Length && iA < a.Length)
                    return false;
                else if (iA >= a.Length || iB >= b.Length)
                    break;
            }

            return true;
        }

        bool IsValid(string[] inputs)
        {
            string a = inputs[0];
            string b = inputs[1];

            return CompareList(a, b);
            Stack<int> aList = new();
            Stack<int> bList = new();

            int iA = 0;
            int iB = 0;

            // while (true)
            // {
            //     if (int.TryParse(a[iA].ToString(), out int num1) && int.TryParse(b[iB].ToString(), out int num2))
            //     {
            //         aList.Push(aList.Pop() + 1);
            //         bList.Push(bList.Pop() + 1);
            //         if (num1 > num2)
            //             return false;
            //     }
            //     else if (a[iA] == '[' && b[iB] == '[')
            //     {
            //         aList.Push(0);
            //         bList.Push(0);
            //     }
            //     else if (a[iA] == ']' && b[iB] == ']')
            //     {
            //         if (aList.Pop() < bList.Pop())
            //             return false;
            //     }
            //     else if (int.TryParse(a[iA].ToString(), out int _num1))
            //     {
            //         iB++;
            //         continue;
            //     }
            //     else if (int.TryParse(b[iB].ToString(), out int _num2))
            //     {
            //         iA++;
            //         continue;
            //     }

            //     iA++;
            //     iB++;

            //     while (iA < a.Length && a[iA] == ',') iA++;
            //     while (iB < b.Length && b[iB] == ',') iB++;

            //     if (iA >= a.Length || iB >= b.Length) break;
            // }

            return true;
        }

        public override object Part1()
        {
            int count = 0;
            for (int i = 0; i < lines.Length; i++)
                if (IsValid(lines[i]))
                    count += i + 1;

            return count;
        }

        public override object Part2()
        {


            return 0;
        }
    }
}
