namespace LianZhao.Media
{
    public partial struct Point2D
    {
        public static bool operator ==(Point2D point1, Point2D point2)
        {
            return Equals(point1, point2);
        }

        public static bool operator !=(Point2D point1, Point2D point2)
        {
            return !(point1 == point2);
        }

        public static bool Equals(Point2D point1, Point2D point2)
        {
            return point1._x == point2._x && point1._y == point2._y;
        }

        public bool Equals(Point2D other)
        {
            return Equals(this, other);
        }

        public override bool Equals(object o)
        {
            if (!(o is Point2D))
            {
                return false;
            }

            var value = (Point2D)o;
            return Equals(this, value);
        }

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode();
        }
    }
}