using System;

namespace AdventOfCode
{
    struct V3
    {
        public int x;
        public int y;
        public int z;

        public static V3[] AllDirections => new V3[] {
            new V3( 1,  0,  0),
            new V3(-1,  0,  0),
            new V3( 0,  1,  0),
            new V3( 0, -1,  0),
            new V3( 0,  0,  1),
            new V3( 0,  0, -1)
        };

        public V3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static V3 operator +(V3 a, V3 b)
        {
            return new V3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static V3 operator -(V3 a, V3 b)
        {
            return new V3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static V3 operator *(V3 a, int scale)
        {
            return new V3(a.x * scale, a.y * scale, a.z * scale);
        }

        public static bool operator ==(V3 a, V3 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(V3 a, V3 b)
        {
            return !(a == b);
        }
        
        public override string ToString()
        {
            return $"{x},{y},{z}";
        }

        public override bool Equals(object obj)
        {
            return (V3)obj == this;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }
    }
}
