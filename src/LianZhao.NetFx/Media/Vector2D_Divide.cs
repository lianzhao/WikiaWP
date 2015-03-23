namespace LianZhao.Media
{
    public partial struct Vector2D
    {
        public static Vector2D Divide(Vector2D vector, double scalar)
        {
            return Multiply(vector, 1.0/scalar);
        }

        public static Vector2D operator /(Vector2D vector, double scalar)
        {
            return Divide(vector, scalar);
        }

        public Vector2D Divide(double scalar)
        {
            return Divide(this, scalar);
        }
    }
}