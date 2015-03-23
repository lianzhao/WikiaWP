namespace LianZhao.Media
{
    public partial struct Vector2D
    {
        public static double CrossProduct(Vector2D vector1, Vector2D vector2)
        {
            return vector1._x * vector2._y - vector1._y * vector2._x;
        }

        public double CrossProduct(Vector2D other)
        {
            return CrossProduct(this, other);
        }
    }
}