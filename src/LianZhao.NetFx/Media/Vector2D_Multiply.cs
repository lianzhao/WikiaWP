namespace LianZhao.Media
{
    public partial struct Vector2D
    {
        public static Vector2D Multiply(Vector2D vector, double scalar)
        {
            return new Vector2D(vector._x*scalar, vector._y*scalar);
        }

        public static Vector2D operator *(Vector2D vector, double scalar)
        {
            return Multiply(vector, scalar);
        }

        public static Vector2D operator *(double scalar, Vector2D vector)
        {
            return Multiply(vector, scalar);
        }
    }
}