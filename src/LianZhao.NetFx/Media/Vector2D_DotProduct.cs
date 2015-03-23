namespace LianZhao.Media
{
    public partial struct Vector2D
    {
        public static double DotProduct(Vector2D vector1, Vector2D vector2)
        {
            return vector1._x*vector2._x + vector1._y*vector2._y;
        }

        public double DotProduct(Vector2D other)
        {
            return DotProduct(this, other);
        }
    }
}