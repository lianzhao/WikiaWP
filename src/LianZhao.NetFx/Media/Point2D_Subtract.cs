namespace LianZhao.Media
{
    public partial struct Point2D
    {
        public static Point2D Subtract(Point2D point, Vector2D vector)
        {
            return new Point2D(point._x - vector._x, point._y - vector._y);
        }

        public static Point2D operator -(Point2D point, Vector2D vector)
        {
            return Subtract(point, vector);
        }

        public Point2D Subtract(Vector2D vector)
        {
            return Subtract(this, vector);
        }

        public static Vector2D Subtract(Point2D point1, Point2D point2)
        {
            return new Vector2D(point1._x - point2._x, point1._y - point2._y);
        }

        public static Vector2D operator -(Point2D point1, Point2D point2)
        {
            return Subtract(point1, point2);
        }

        public Vector2D Subtract(Point2D other)
        {
            return Subtract(this, other);
        }
    }
}