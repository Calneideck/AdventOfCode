using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day15 : Day
    {
        class Node
        {
            public (int x, int y) pos;
            public int tile;
            public Node parent;
            public bool oxygen;
        }

        private IEnumerable<long> codeList;
        private List<Node> nodes = new List<Node>();
        private Node oxygenSystemNode;

        public override string Part1()
        {
            codeList = File.ReadAllText("Input/15.txt").Split(',').Select(s => long.Parse(s));
            LCVM vm = new LCVM(codeList.ToArray());

            Queue<Node> openList = new Queue<Node>();
            Node currentNode = new Node()
            {
                pos = (0, 0),
                tile = 0
            };
            var pos = currentNode.pos;
            openList.Enqueue(currentNode);
            nodes.Add(currentNode);
            int pathToOxygenLength = 0;

            while (openList.Any())
            {
                currentNode = openList.Dequeue();

                // Move to position
                if (pos != currentNode.pos)
                {
                    var pathToStart = new List<Node>();
                    var n = currentNode;
                    while (n.pos != (0, 0))
                    {
                        pathToStart.Add(n);
                        n = n.parent;
                    }
                    pathToStart.Reverse();

                    var dronePathToStart = new Queue<Node>();
                    n = nodes.Find(n => n.pos == pos);
                    while (n.pos != (0, 0))
                    {
                        n = n.parent;
                        dronePathToStart.Enqueue(n);
                    }

                    foreach (var node in pathToStart)
                        dronePathToStart.Enqueue(node);

                    while (pos != currentNode.pos)
                    {
                        var nextNode = dronePathToStart.Dequeue();
                        var directionToTake = (nextNode.pos.x - pos.x, nextNode.pos.y - pos.y);
                        var dir = GetDirCode(directionToTake);
                        vm.AddInput(dir);
                        vm.GetCodeResult();
                        vm.GetOutput();

                        pos = nextNode.pos;
                    }
                }

                // Check surroundings
                for (int i = 1; i <= 4; i++)
                {
                    var change = GetDir(i);
                    var newPos = (currentNode.pos.x + change.x, currentNode.pos.y + change.y);
                    if (nodes.Find(n => n.pos == newPos) == null)
                    {
                        vm.AddInput(i);
                        vm.GetCodeResult();
                        var output = vm.GetOutput();

                        Node newNode = new Node()
                        {
                            pos = newPos,
                            tile = (int)output,
                            parent = currentNode
                        };

                        nodes.Add(newNode);

                        if (output == 2)
                        {
                            oxygenSystemNode = newNode;
                            var pathToStart = new Queue<Node>();
                            Node n = newNode;
                            while (n.pos != (0, 0))
                            {
                                n = n.parent;
                                pathToStart.Enqueue(n);
                            }

                            pathToOxygenLength = pathToStart.Count;
                        }

                        if (output == 1 || output == 2)
                        {
                            openList.Enqueue(newNode);
                            // Return to current node
                            vm.AddInput(Opposite(i));
                            vm.GetCodeResult();
                            vm.GetOutput();
                        }
                    }
                }
            }

            DrawArea(currentNode.pos);
            return pathToOxygenLength.ToString();
        }

        public override string Part2()
        {
            var emptySpace = new List<Node>(nodes.Where(n => n.tile == 1));
            var openList = new List<Node>();
            var toAdd = new List<Node>();
            openList.Add(oxygenSystemNode);
            oxygenSystemNode.oxygen = true;

            int time = 0;
            while (emptySpace.Any())
            {
                while (openList.Any())
                {
                    var n = openList[0];
                    openList.RemoveAt(0);

                    for (int i = 1; i <= 4; i++)
                    {
                        var (xDir, yDir) = GetDir(i);
                        var newPos = (n.pos.x + xDir, n.pos.y + yDir);
                        var nextNode = emptySpace.Find(node => node.pos == newPos);
                        if (nextNode != null && !nextNode.oxygen)
                        {
                            nextNode.oxygen = true;
                            toAdd.Add(nextNode);
                            emptySpace.Remove(nextNode);
                        }
                    }
                }

                openList.AddRange(toAdd);
                toAdd.Clear();
                
                ++time;
            }

            return time.ToString();
        }

        void DrawArea((int x, int y) pos)
        {
            int minX = nodes.Min(n => n.pos.x);
            int maxX = nodes.Max(n => n.pos.x);
            int minY = nodes.Min(n => n.pos.y);
            int maxY = nodes.Max(n => n.pos.y);
            Console.Clear();

            for (int y = maxY; y >= minY; y--)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    int tile = -1;
                    var n = nodes.Find(n => n.pos == (x, y));
                    if (n != null)
                        tile = n.tile;

                    char c = tile switch
                    {
                        0 => '█',
                        1 => '.',
                        2 => '$',
                        _ => ' '
                    };

                    if (x == pos.x && y == pos.y)
                        c = 'X';

                    Console.Write(c);
                }

                Console.WriteLine();
            }
        }

        int Opposite(int dir)
        {
            return dir switch
            {
                1 => 2,
                2 => 1,
                3 => 4,
                4 => 3
            };
        }

        (int x, int y) GetDir(int dir)
        {
            return dir switch
            {
                1 => (0, 1),
                2 => (0, -1),
                3 => (-1, 0),
                4 => (1, 0)
            };
        }

        int GetDirCode((int, int) dir)
        {
            return dir switch
            {
                (0, 1) => 1,
                (0, -1) => 2,
                (-1, 0) => 3,
                (1, 0) => 4
            };
        }
    }
}
