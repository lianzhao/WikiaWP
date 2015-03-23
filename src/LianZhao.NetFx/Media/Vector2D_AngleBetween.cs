using System;

namespace LianZhao.Media
{
    public partial struct Vector2D
    {
        public double AngleBetween(Vector2D other)
        {
            return AngleBetween(this, other);
        }

        public static double AngleBetween(Vector2D vector1, Vector2D vector2)
        {
            var sin = vector1._x*vector2._y - vector2._x*vector1._y;
            var cos = vector1._x*vector2._x + vector1._y*vector2._y;

            return Math.Atan2(sin, cos) * (180 / Math.PI);
        }
    }
}