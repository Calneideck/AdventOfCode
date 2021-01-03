using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day20 : Day
    {
        string[] lines = File.ReadAllLines("Input/20.txt").ToArray();

        Dictionary<int, string[]> borders = new Dictionary<int, string[]>();
        Dictionary<int, char[,]> tiles = new Dictionary<int, char[,]>();
        Dictionary<int, (V2 pos, int rotate, bool flipped)> placed = new Dictionary<int, (V2, int, bool)>();
        Queue<int> tilesToCheck = new Queue<int>();

        Regex numReg = new Regex(@"\d+");

        char[,] FlipImage(char[,] tile)
        {
            int size = tile.GetLength(0);
            char[,] newTile = new char[size, size];
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                    newTile[y, x] = tile[y, size - 1 - x];

            return newTile;
        }

        char[,] RotateImage(char[,] tile, int amount)
        {
            int size = tile.GetLength(0);
            char[,] newTile = (char[,])tile.Clone();

            for (int i = 0; i < amount; i++)
            {
                for (int y = 0; y < size; y++)
                    for (int x = 0; x < size; x++)
                        newTile[y, x] = tile[x, size - 1 - y];

                tile = (char[,])newTile.Clone();
            }

            return newTile;
        }

        string[] Rotate(string[] tile, int amount)
        {
            string[] newTile = (string[])tile.Clone();

            for (int i = 0; i < amount; i++)
            {
                for (int x = 0; x < 4; x++)
                {
                    newTile[x] = tile[(x + 1) % 4];
                    if (x == 1 || x == 3)
                        newTile[x] = Reverse(newTile[x]);
                }

                tile = (string[])newTile.Clone();
            }

            return newTile;
        }

        string[] Flip(string[] tile)
        {
            string[] newTile = (string[])tile.Clone();
            newTile[0] = Reverse(newTile[0]);
            newTile[2] = Reverse(newTile[2]);
            newTile[1] = tile[3];
            newTile[3] = tile[1];
            return newTile;
        }

        void PlaceTiles()
        {
            var targetTile = tilesToCheck.Dequeue();
            string[] tileBorders = placed[targetTile].flipped ? Flip(borders[targetTile]) : borders[targetTile];
            tileBorders = Rotate(tileBorders, placed[targetTile].rotate);

            var nextTiles = borders.Where(kv => !placed.Keys.Contains(kv.Key));

            for (int i = 0; i < 4; i++)
            {
                string border = tileBorders[i];
                int otherIndex = (i + 2) % 4;
                bool foundMatch = false;

                foreach (var otherTile in nextTiles)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        string[] otherBorders = Rotate(otherTile.Value, j);
                        if (border == otherBorders[otherIndex])
                        {
                            placed.Add(otherTile.Key, (placed[targetTile].pos + V2.Directions[i], j, false));
                            tilesToCheck.Enqueue(otherTile.Key);
                            foundMatch = true;
                            break;
                        }
                        else
                        {
                            otherBorders = Rotate(Flip(otherTile.Value), j);
                            if (border == otherBorders[otherIndex])
                            {
                                placed.Add(otherTile.Key, (placed[targetTile].pos + V2.Directions[i], j, true));
                                tilesToCheck.Enqueue(otherTile.Key);
                                foundMatch = true;
                                break;
                            }
                        }
                    }

                    if (foundMatch)
                        break;
                }
            }
        }

        char[,] StitchMap()
        {
            int edgeCount = (int)Math.Sqrt(borders.Count);
            char[,] stitchedMap = new char[edgeCount * 8, edgeCount * 8];
            int minX = placed.Values.Min(tile => tile.pos.x);
            int minY = placed.Values.Min(tile => tile.pos.y);

            foreach (var tile in placed)
            {
                V2 offset = tile.Value.pos - new V2(minX, minY);
                char[,] newImage = tile.Value.flipped ? FlipImage(tiles[tile.Key]) : tiles[tile.Key];
                newImage = RotateImage(newImage, tile.Value.rotate);

                for (int y = 0; y < 8; y++)
                    for (int x = 0; x < 8; x++)
                        stitchedMap[offset.y * 8 + y, offset.x * 8 + x] = newImage[y + 1, x + 1];
            }

            return stitchedMap;
        }

        public override object Part1()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                int id = int.Parse(numReg.Match(lines[i]).ToString());
                borders.Add(id, new string[4]);
                tiles.Add(id, new char[10, 10]);

                StringBuilder s1 = new StringBuilder();
                StringBuilder s2 = new StringBuilder();

                for (int y = 0; y < 10; y++)
                {
                    if (y == 0)
                        borders[id][0] = lines[i + 1 + y];
                    else if (y == 9)
                        borders[id][2] = lines[i + 1 + y];

                    s1.Append(lines[i + 1 + y][0]);
                    s2.Append(lines[i + 1 + y][9]);

                    for (int x = 0; x < 10; x++)
                        tiles[id][y, x] = lines[i + 1 + y][x];
                }

                borders[id][3] = s1.ToString();
                borders[id][1] = s2.ToString();

                i += 11;
            }

            long corners = 1;
            var allBorders = new List<string>();
            foreach (var border in borders.Values)
                allBorders.AddRange(border);

            foreach (var tile in borders)
            {
                int borderMatch = 0;
                foreach (var border in tile.Value)
                {
                    string flip = Reverse(border);
                    borderMatch += allBorders.Count(b => b == border) + allBorders.Count(b => b == flip);
                }

                if (borderMatch == 6)
                    corners *= tile.Key;
            }

            return corners;
        }

        public override object Part2()
        {
            placed.Add(borders.Keys.First(), (V2.Zero, 0, false));
            tilesToCheck.Enqueue(borders.Keys.First());
            while (placed.Keys.Count != borders.Keys.Count)
                PlaceTiles();

            char[,] stitchedMap = StitchMap();

            int size = stitchedMap.GetLength(0);
            Regex snakeRegex = new Regex($"(?<=#.{{{size - 19}}})#.{{4}}##.{{4}}##.{{4}}###(?=.{{{size - 19}}}#.{{2}}#.{{2}}#.{{2}}#.{{2}}#.{{2}}#)");

            for (int i = 0; i < 4; i++)
            {
                var newMap = RotateImage(stitchedMap, i);
                StringBuilder sb = new StringBuilder();
                for (int y = 0; y < size; y++)
                    for (int x = 0; x < size; x++)
                        sb.Append(newMap[y, x]);

                string snakeString = sb.ToString();
                var matches = snakeRegex.Matches(snakeString);
                if (matches.Count > 0)
                    return snakeString.Count(c => c == '#') - matches.Count * 15;
                else
                {
                    newMap = RotateImage(FlipImage(stitchedMap), i);
                    sb = new StringBuilder();
                    for (int y = 0; y < size; y++)
                        for (int x = 0; x < size; x++)
                            sb.Append(newMap[y, x]);

                    snakeString = sb.ToString();
                    matches = snakeRegex.Matches(snakeString);
                    if (matches.Count > 0)
                        return snakeString.Count(c => c == '#') - matches.Count * 15;
                }
            }

            return -1;
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}

/*
                  # 
#    ##    ##    ###
 #  #  #  #  #  #   
*/
