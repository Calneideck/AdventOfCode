using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day4 : Day
    {
        string[] lines = File.ReadAllLines("Input/4.txt");

        public override object Part1()
        {
            List<string> words = new List<string>();
            int count = 0;
            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    if (words.Count == 7)
                        count++;

                    words.Clear();
                }
                else
                {
                    var temp = line.Split(' ');
                    foreach (var w in temp)
                    {
                        string word = w.Substring(0, 3);
                        if (!words.Contains(word) && word != "cid")
                            words.Add(word);
                    }
                }
            }

            return count.ToString();

        }

        bool isvalid(string word, string res)
        {
            int num = 0;
            switch (word)
            {
                case "byr":
                    num = int.Parse(res);
                    if (num >= 1920 && num <= 2002)
                        return true;
                    break;

                case "iyr":
                    num = int.Parse(res);
                    if (num >= 2010 && num <= 2020)
                        return true;
                    break;

                case "eyr":
                    num = int.Parse(res);
                    if (num >= 2020 && num <= 2030)
                        return true;
                    break;

                case "hgt":
                    string meas = res.Substring(res.Length - 2);
                    if (meas != "cm" && meas != "in") return false;

                    num = int.Parse(res.Substring(0, res.Length - 2));
                    if (meas == "cm" && num >= 150 && num <= 193)
                        return true;
                    else if (meas == "in" && num >= 59 && num <= 76)
                        return true;
                    break;

                case "hcl":
                    Regex r = new Regex("^#[0-9a-f]{6}$");
                    return r.IsMatch(res);

                case "ecl":
                    return res == "amb" || res == "blu" || res == "brn" || res == "gry" || res == "grn" || res == "hzl" || res == "oth";

                case "pid":
                    Regex r2 = new Regex("^[0-9]{9}$");
                    return r2.IsMatch(res);
            }

            return false;
        }

        public override object Part2()
        {
            List<string> words = new List<string>();
            int count = 0;
            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    if (words.Count == 7)
                        count++;

                    words.Clear();
                }
                else
                {
                    var temp = line.Split(' ');
                    foreach (var w in temp)
                    {
                        var check = w.Split(':');
                        string word = check[0];
                        string result = check[1];
                        bool good = isvalid(word, result);

                        if (good && !words.Contains(word))
                            words.Add(word);
                    }
                }
            }

            return count.ToString();
        }
    }
}
