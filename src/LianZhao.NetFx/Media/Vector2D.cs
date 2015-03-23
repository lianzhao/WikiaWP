using System;

namespace LianZhao.Media
{
    public partial struct Vector2D : IFormattable, IEquatable<Vector2D>
    {
        internal readonly double _x;

        internal readonly double _y;

        public Vector2D(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public double X { get { return _x; } }

        public double Y { get { return _y; } }

        public double Length
        {
            get { return Math.Sqrt(_x * _x + _y * _y); }
        }

        public double LengthSquared
        {
            get { return _x*_x + _y*_y; }
        }
    }
}