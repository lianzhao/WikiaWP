namespace LianZhao.Media
{
    public partial struct Point2D
    {
        public static Point2D Add(Point2D point, Vector2D vector)
        {
            return new Point2D(point._x + vector._x, point._y + vector._y);
        }

        public static Point2D operator +(Point2D point, Vector2D vector)
        {
            return Add(point, vector);
        }

        public Point2D Add(Vector2D vector)
        {
            return Add(this, vector);
        }
    }
}