namespace LianZhao.Media
{
    public partial struct Vector2D
    {
        public static Vector2D Add(Vector2D vector1, Vector2D vector2)
        {
            return new Vector2D(vector1._x + vector2._x, vector1._y + vector2._y);
        }

        public static Vector2D operator +(Vector2D vector1, Vector2D vector2)
        {
            return Add(vector1, vector2);
        }

        public Vector2D Add(Vector2D other)
        {
            return Add(this, other);
        }

        public static Point2D Add(Vector2D vector, Point2D point)
        {
            return new Point2D(vector._x + point._x, vector._y + point._y);
        }

        public static Point2D operator +(Vector2D vector, Point2D point)
        {
            return Add(vector, point);
        }

        public Point2D Add(Point2D point)
        {
            return Add(this, point);
        }
    }
}