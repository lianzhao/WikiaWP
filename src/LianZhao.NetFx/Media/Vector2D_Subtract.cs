namespace LianZhao.Media
{
    public partial struct Vector2D
    {
        public static Vector2D Subtract(Vector2D vector1, Vector2D vector2)
        {
            return new Vector2D(vector1._x - vector2._x, vector1._y - vector2._y);
        }

        public static Vector2D operator -(Vector2D vector1, Vector2D vector2)
        {
            return Subtract(vector1, vector2);
        }

        public Vector2D Subtract(Vector2D other)
        {
            return Subtract(this, other);
        }

        public static Point2D Subtract(Vector2D vector, Point2D point)
        {
            return new Point2D(vector._x - point._x, vector._y - point._y);
        }

        public static Point2D operator -(Vector2D vector, Point2D point)
        {
            return Subtract(vector, point);
        }

        public Point2D Subtract(Point2D point)
        {
            return Subtract(this, point);
        }
    }
}