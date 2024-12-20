﻿using System;

namespace AdventOfCode
{
    public struct V2
    {
        public int x;
        public int y;

        public V2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int Cost => Math.Abs(x) + Math.Abs(y);

        public static V2 Zero => new V2(0, 0);

        public static V2 Up => new V2(0, -1);

        public static V2 Down => new V2(0, 1);

        public static V2 Left => new V2(-1, 0);

        public static V2 Right => new V2(1, 0);

        public static V2 UpLeft => new V2(-1, -1);

        public static V2 UpRight => new V2(1, -1);

        public static V2 DownLeft => new V2(-1, 1);

        public static V2 DownRight => new V2(1, 1);

        public static V2[] Directions => new V2[] { Up, Right, Down, Left };

        public static V2[] Diagonals => new V2[] { UpLeft, UpRight, DownRight, DownLeft };

        public static V2[] AllDirections => new V2[] { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft };

        public static bool Opposite(V2 a, V2 b)
        {
            if (a == b || a == V2.Zero || b == V2.Zero) return false;

            return a.x + b.x == 0 || a.y + b.y == 0;
        }

        public static V2 operator +(V2 a, V2 b)
        {
            return new V2(a.x + b.x, a.y + b.y);
        }

        public static V2 operator -(V2 a, V2 b)
        {
            return new V2(a.x - b.x, a.y - b.y);
        }

        public static V2 operator *(V2 a, int scale)
        {
            return new V2(a.x * scale, a.y * scale);
        }

        public static bool operator ==(V2 a, V2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(V2 a, V2 b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{x},{y}";
        }

        public override bool Equals(object obj)
        {
            return (V2)obj == this;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }
}
