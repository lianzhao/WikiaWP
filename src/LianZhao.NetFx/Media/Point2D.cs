using System;

namespace LianZhao.Media
{
    public partial struct Point2D : IFormattable, IEquatable<Point2D>
    {
        internal readonly double _x;

        internal readonly double _y;

        public Point2D(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public double X { get { return _x; } }

        public double Y { get { return _y; } }
    }
}
