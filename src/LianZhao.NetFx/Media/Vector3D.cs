using System;

namespace LianZhao.Media
{
    public partial struct Vector3D : IFormattable, IEquatable<Vector3D>
    {
        internal readonly double _x;

        internal readonly double _y;

        internal readonly double _z;

        public Vector3D(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public double X { get { return _x; } }

        public double Y { get { return _y; } }

        public double Z { get { return _z; } }

        public double Length
        {
            get { return Math.Sqrt(_x * _x + _y * _y + _z * _z); }
        }

        public double LengthSquared
        {
            get { return _x*_x + _y*_y + _z*_z; }
        }
    }
}