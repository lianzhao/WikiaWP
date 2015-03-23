using System;

namespace LianZhao.Media
{
    public partial struct Point3D : IFormattable, IEquatable<Point3D>
    {
        internal readonly double _x;

        internal readonly double _y;

        internal readonly double _z;

        public Point3D(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public double X { get { return _x; } }

        public double Y { get { return _y; } }

        public double Z { get { return _z; } }
    }
}
