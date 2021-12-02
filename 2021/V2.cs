namespace AdventOfCode
{
    struct V2
    {
        public int x;
        public int y;

        public V2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator !=(V2 a, V2 b)
        {
            return !(a == b);
        }

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

        public static V2 operator +(V2 a, V2 b)
        {
            return new V2()
            {
                x = a.x + b.x,
                y = a.y + b.y
            };
        }

        public static V2 operator -(V2 a, V2 b)
        {
            return new V2()
            {
                x = a.x - b.x,
                y = a.y - b.y
            };
        }

        public static V2 operator *(V2 a, int scale)
        {
            return new V2()
            {
                x = a.x * scale,
                y = a.y * scale
            };
        }

        public static bool operator ==(V2 a, V2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public override string ToString()
        {
            return $"{x},{y}";
        }
    }
}
