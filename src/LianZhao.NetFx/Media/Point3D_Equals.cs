namespace LianZhao.Media
{
    public partial struct Point3D
    {
        public static bool operator ==(Point3D point1, Point3D point2)
        {
            return Equals(point1, point2);
        }

        public static bool operator !=(Point3D point1, Point3D point2)
        {
            return !(point1 == point2);
        }

        public static bool Equals(Point3D point1, Point3D point2)
        {
            return point1._x == point2._x && point1._y == point2._y && point1._z == point2._z;
        }

        public bool Equals(Point3D other)
        {
            return Equals(this, other);
        }

        public override bool Equals(object o)
        {
            if (!(o is Point3D))
            {
                return false;
            }

            var value = (Point3D)o;
            return Equals(this, value);
        }

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode() ^ _z.GetHashCode();
        }
    }
}