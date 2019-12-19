using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day18 : Day
    {
        public class Node
        {
            public V2 pos;
            public char tile;
            public bool isKey;
            public bool isDoor;
            public List<Node> path;
        }

        /*class MapState
        {
            public List<Node> nodes;
            public List<char> keys;
            public Node currentNode;
            public int steps;

            public MapState() { }

            public MapState(MapState other, Node nextNode)
            {
                keys = new List<char>(other.keys);
                nodes = new List<Node>();
                foreach (var node in other.nodes)
                    nodes.Add(node.Clone());

                steps = other.steps + nextNode.path.Count;
                currentNode = NodeFromPos(nextNode.x, nextNode.y);
                ProcessNode(currentNode);
            }

            public int StepsToEnd()
            {
                while (keys.Count < KeyCount)
                {
                    var paths = Search(currentNode);
                    bool found = false;

                    if (paths.Count == 0)
                        throw new Exception("No Paths");
                    else if (paths.Count == 1)
                    {
                        currentNode = paths[0];
                        steps += currentNode.path.Count;
                        ProcessNode(currentNode);
                        found = true;
                    }
                    else
                    {
                        foreach (var pathNode in paths)
                        {
                            bool inAllOtherPaths = false;
                            foreach (var otherNode in paths)
                            {
                                if (pathNode == otherNode)
                                    continue;

                                if (!otherNode.path.Contains(pathNode))
                                {
                                    inAllOtherPaths = false;
                                    break;
                                }
                            }

                            if (inAllOtherPaths)
                            {
                                currentNode = pathNode;
                                steps += currentNode.path.Count;
                                ProcessNode(currentNode);
                                found = true;
                                break;
                            }
                        }
                    }

                    if (!found)
                    {
                        var states = new List<MapState>();

                        foreach (var pathNode in paths)
                        {
                            var state = new MapState(this, pathNode);
                            states.Add(state);
                        }

                        return states.Min(s => s.StepsToEnd());
                    }
                }

                return steps;
            }

            void ProcessNode(Node n)
            {
                if (n.isKey)
                    keys.Add(n.tile);
                else if (!n.isDoor)
                    throw new Exception();

                n.tile = '.';
                n.isKey = false;
                n.isDoor = false;
            }

            List<Node> Search(Node startNode)
            {
                HashSet<Node> closed = new HashSet<Node>();
                Queue<Node> open = new Queue<Node>();
                open.Enqueue(startNode);

                var paths = new List<Node>();
                foreach (var n in nodes)
                    n.path.Clear();

                while (open.Any())
                {
                    var currentNode = open.Dequeue();
                    closed.Add(currentNode);

                    foreach (var dir in dirs)
                    {
                        var (x, y) = (currentNode.x + dir.x, currentNode.y + dir.y);
                        Node nextNode = NodeFromPos(x, y);
                        if (nextNode != null && nextNode.tile != '#' && !closed.Contains(nextNode) && !open.Contains(nextNode))
                        {
                            nextNode.path.AddRange(currentNode.path);
                            nextNode.path.Add(currentNode);

                            if ((nextNode.isDoor && keys.Contains(nextNode.tile.ToString().ToLower()[0])) || nextNode.isKey)
                                paths.Add(nextNode);

                            if (nextNode.isDoor)
                                closed.Add(nextNode);
                            else
                                open.Enqueue(nextNode);
                        }
                    }
                }

                return paths;
            }

            Node NodeFromPos(int x, int y)
            {
                return nodes.Find(n => n.x == x && n.y == y);
            }
        }

        private static (int x, int y)[] dirs = new (int x, int y)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };*/

        struct Path
        {
            public V2[] points;
            public int steps;
            public char[] reqKeys;
        }

        private List<Node> nodes = new List<Node>();
        private List<Node> keyNodes = new List<Node>();
        private List<char> allKeys = new List<char>();
        private List<Path> paths = new List<Path>();
        private Node startNode;

        public override string Part1()
        {
            var input = File.ReadAllLines("Input/18.txt");

            Regex keyR = new Regex("[a-z]");
            Regex doorR = new Regex("[A-Z]");

            int y = 0;
            foreach (var line in input)
            {
                int x = 0;
                var c = line.ToCharArray();

                foreach (var tile in c)
                {
                    Node n = new Node()
                    {
                        pos = new V2(x, y),
                        tile = tile,
                        isDoor = doorR.IsMatch(tile.ToString()),
                        isKey = keyR.IsMatch(tile.ToString()),
                        path = new List<Node>()
                    };

                    nodes.Add(n);

                    if (tile == '@')
                        startNode = n;

                    if (n.isKey)
                        allKeys.Add(n.tile);

                    x++;
                }

                y++;
            }

            Console.WriteLine();

            keyNodes = nodes.Where(n => n.isKey).ToList();
            FindPaths();
            Console.WriteLine("Paths found.");

            int steps = ProcessPath(startNode.pos, new char[0], 0);

            /*var state = new MapState()
            {
                currentNode = currentNode,
                nodes = nodes,
                keys = new List<char>()
            };*/

            return steps.ToString();
        }

        int ProcessPath(V2 pos, char[] keys, int steps)
        {
            var remainingKeys = allKeys.Except(keys);
            if (remainingKeys.Any())
            {
                var remainingKeyNodes = keyNodes.Where(n => remainingKeys.Contains(n.tile));
                return remainingKeyNodes.Min(keyNode =>
                {
                    var path = paths.Find(p => p.points.Contains(pos) && p.points.Contains(keyNode.pos));

                    if (path.reqKeys.Intersect(keys).Count() == keys.Length)

                    /*bool hasKeys = true;
                    foreach (var reqKey in path.reqKeys)
                        if (!keys.Contains(reqKey))
                        {
                            hasKeys = false;
                            break;
                        }

                    if (hasKeys)*/
                        return ProcessPath(keyNode.pos, keys.Append(keyNode.tile).ToArray(), steps + path.steps);
                    else
                        return int.MaxValue;
                });
            }
            else
                return steps;
        }

        void FindPaths()
        {
            HashSet<char> doneKeys = new HashSet<char>();
            var dirs = V2.Directions;
            paths.Clear();

            keyNodes.Insert(0, startNode);

            foreach (Node n in keyNodes)
            {
                HashSet<Node> closed = new HashSet<Node>();
                Queue<Node> open = new Queue<Node>();
                open.Enqueue(n);

                if (n.isKey)
                    doneKeys.Add(n.tile);

                foreach (var node in nodes)
                    node.path.Clear();

                while (open.Any())
                {
                    var currentNode = open.Dequeue();
                    closed.Add(currentNode);

                    foreach (var dir in dirs)
                    {
                        Node nextNode = nodes.Find(n => n.pos == currentNode.pos + dir);
                        if (nextNode != null && nextNode.tile != '#' && !closed.Contains(nextNode) && !open.Contains(nextNode))
                        {
                            nextNode.path.AddRange(currentNode.path);
                            nextNode.path.Add(currentNode);

                            if (nextNode.isKey && !doneKeys.Contains(nextNode.tile))
                            {
                                paths.Add(new Path()
                                {
                                    points = new V2[] { n.pos, nextNode.pos },
                                    steps = nextNode.path.Count,
                                    reqKeys = nextNode.path.Where(n => n.isDoor).Select(n => n.tile.ToString().ToLower()[0]).ToArray()
                                });
                            }

                            open.Enqueue(nextNode);
                        }
                    }
                }
            }

            keyNodes.RemoveAt(0);
        }
    }
}
