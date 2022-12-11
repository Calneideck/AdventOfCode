using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Monke
    {
        public List<long> items;
        public Func<long, long> calc;
        public Func<long, bool> test;
        public int testPass;
        public int testFail;
    }

    class Day11 : Day
    {
        List<Monke> GetInput()
        {
            return new List<Monke>() {
                new Monke() {
                    items = new List<long>() { 83, 88, 96, 79, 86, 88, 70 },
                    calc =  (x) => x * 5,
                    test = (x) => x % 11 == 0,
                    testPass = 2,
                    testFail = 3
                },
                new Monke() {
                    items = new List<long>() { 59, 63, 98, 85, 68, 72 },
                    calc =  (x) => x * 11,
                    test = (x) => x % 5 == 0,
                    testPass = 4,
                    testFail =  0
                },
                new Monke() {
                    items = new List<long>() { 90, 79, 97, 52, 90, 94, 71, 70 },
                    calc = (x) => x + 2,
                    test = (x) => x % 19 == 0,
                    testPass = 5,
                    testFail = 6
                },
                new Monke() {
                    items = new List<long>() { 97, 55, 62 },
                    calc = (x) => x + 5,
                    test = (x) => x % 13 == 0,
                    testPass = 2,
                    testFail = 6
                },
                new Monke() {
                    items = new List<long>() { 74, 54, 94, 76 },
                    calc = (x) => x * x,
                    test = (x) => x % 7 == 0,
                    testPass = 0,
                    testFail = 3
                },
                new Monke() {
                    items = new List<long>() { 58 },
                    calc = (x) => x + 4,
                    test = (x) => x % 17 == 0,
                    testPass = 7,
                    testFail = 1
                },
                new Monke() {
                    items = new List<long>() { 66, 63 },
                    calc = (x) => x + 6,
                    test = (x) => x % 2 == 0,
                    testPass = 7,
                    testFail = 5
                },
                new Monke() {
                    items = new List<long>() { 56, 56, 90, 96, 68 },
                    calc = (x) => x + 7,
                    test = (x) => x % 3 == 0,
                    testPass = 4,
                    testFail = 1
                },
            };
        }

        public override object Part1()
        {
            List<Monke> monkes = GetInput();
            Dictionary<int, long> counts = new();
            for (int i = 0; i < monkes.Count; i++)
                counts.Add(i, 0);

            for (int i = 0; i < 20; i++)
                for (int j = 0; j < monkes.Count; j++)
                {
                    Monke thisMonke = monkes[(int)j];
                    while (thisMonke.items.Count > 0)
                    {
                        long old = thisMonke.items[0];
                        thisMonke.items.RemoveAt(0);
                        counts[j]++;

                        old = thisMonke.calc(old);
                        old = (long)Math.Floor(old / 3f);
                        if (thisMonke.test(old))
                            monkes[thisMonke.testPass].items.Add(old);
                        else
                            monkes[thisMonke.testFail].items.Add(old);
                    }
                }

            List<long> top = new(counts.Values);
            top.Sort();
            top.Reverse();
            return top[0] * top[1];
        }

        public override object Part2()
        {
            List<Monke> monkes = GetInput();
            Dictionary<int, long> counts = new();
            for (int i = 0; i < monkes.Count; i++)
                counts.Add(i, 0);

            long LCM = 9699690; // LCM of all monke test cases

            for (int i = 0; i < 10000; i++)
                for (int j = 0; j < monkes.Count; j++)
                {
                    Monke thisMonke = monkes[j];
                    while (thisMonke.items.Count > 0)
                    {
                        long old = thisMonke.items[0];
                        thisMonke.items.RemoveAt(0);
                        counts[j]++;

                        old = thisMonke.calc(old);
                        old %= LCM; 
                        if (thisMonke.test(old))
                            monkes[thisMonke.testPass].items.Add(old);
                        else
                            monkes[thisMonke.testFail].items.Add(old);
                    }
                }

            List<long> top = new(counts.Values);
            top.Sort();
            top.Reverse();
            return top[0] * top[1];
        }
    }
}
