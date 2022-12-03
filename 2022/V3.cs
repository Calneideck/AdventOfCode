namespace AdventOfCode
{
    struct V3
    {
        public int x;
        public int y;
        public int z;

        public V3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static bool operator !=(V3 a, V3 b)
        {
            return !(a == b);
        }

        public static V3 operator +(V3 a, V3 b)
        {
            return new V3()
            {
                x = a.x + b.x,
                y = a.y + b.y,
                z = a.z + b.z
            };
        }

        public static V3 operator -(V3 a, V3 b)
        {
            return new V3()
            {
                x = a.x - b.x,
                y = a.y - b.y,
                z = a.z - b.z
            };
        }

        public static V3 operator *(V3 a, int scale)
        {
            return new V3()
            {
                x = a.x * scale,
                y = a.y * scale
            };
        }

        public static bool operator ==(V3 a, V3 b)
        {
            return a.x == b.x && a.z == b.z && a.z == b.z;
        }

        
        public override string ToString()
        {
            return $"{x},{y},{z}";
        }

        public override bool Equals(object obj)
        {
            return (V3)obj == this;
        }
    }
}
