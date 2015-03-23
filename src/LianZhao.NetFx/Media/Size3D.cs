using System;

namespace LianZhao.Media
{
    public partial struct Size3D
    {
        internal readonly double _x;

        internal readonly double _y;

        internal readonly double _z;

        public static readonly Size3D Empty = new Size3D();

        public Size3D(double x, double y, double z)
        {
            if (x < 0 || y < 0 || z < 0)
            {
                throw new ArgumentException("Dimension can not be nagative");
            }

            _x = x;
            _y = y;
            _z = z;
        }

        public double X { get { return _x; } }

        public double Y { get { return _y; } }

        public double Z { get { return _z; } }

        public bool IsEmpty
        {
            get { return _x == 0 && _y == 0 && _z == 0; }
        }
    }
}