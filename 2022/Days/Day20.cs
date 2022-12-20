using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day20 : Day
    {
        string[] lines = File.ReadAllLines("Input/20.txt");

        class Obj
        {
            public long number;
            public int startIndex;
            public Obj prev;
            public Obj next;

            public Obj(long number, int startIndex)
            {
                this.number = number;
                this.startIndex = startIndex;
            }
        }

        List<Obj> GetObjects(int mult)
        {
            List<Obj> objects = new();

            for (int i = 0; i < lines.Length; i++)
                objects.Add(new Obj(long.Parse(lines[i]) * mult, i));

            for (int i = 0; i < lines.Length; i++)
            {
                objects[i].prev = objects[(i - 1 + lines.Length) % lines.Length];
                objects[i].next = objects[(i + 1) % lines.Length];
            }

            return objects;
        }

        void Mix(List<Obj> objects)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                Obj thisObj = objects[i];

                Obj node = thisObj;
                long toMove = Math.Abs(thisObj.number) % (lines.Length - 1);
                if (toMove == 0)
                    continue;

                for (int j = 0; j < toMove; j++)
                    node = thisObj.number > 0 ? node.next : node.prev;

                thisObj.prev.next = thisObj.next;
                thisObj.next.prev = thisObj.prev;

                if (thisObj.number > 0)
                {
                    thisObj.prev = node;
                    thisObj.next = node.next;
                }
                else
                {
                    thisObj.next = node;
                    thisObj.prev = node.prev;
                }

                thisObj.prev.next = thisObj;
                thisObj.next.prev = thisObj;
            }
        }

        long GetCoords(List<Obj> objects)
        {
            long count = 0;
            Obj zeroNode = objects.Find(o => o.number == 0);

            for (int i = 1; i <= 3000; i++)
            {
                zeroNode = zeroNode.next;
                if (i % 1000 == 0)
                    count += zeroNode.number;
            }

            return count;
        }

        public override object Part1()
        {
            List<Obj> objects = GetObjects(1);

            Mix(objects);

            return GetCoords(objects);
        }

        public override object Part2()
        {
            List<Obj> objects = GetObjects(811589153);

            for (int i = 0; i < 10; i++)
                Mix(objects);

            return GetCoords(objects);
        }
    }
}
