namespace LianZhao.Media
{
    public partial struct Vector2D
    {
        public static Vector2D Nagate(Vector2D vector)
        {
            return new Vector2D(-vector._x, -vector._y);
        }

        public static Vector2D operator -(Vector2D vector)
        {
            return Nagate(vector);
        }

        public Vector2D Nagate()
        {
            return Nagate(this);
        }
    }
}