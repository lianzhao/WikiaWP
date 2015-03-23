namespace LianZhao.Media
{
    public partial struct Point3D
    {
        public static Point3D Subtract(Point3D point, Vector3D vector)
        {
            return new Point3D(point._x - vector._x, point._y - vector._y, point._z - vector._z);
        }

        public static Point3D operator -(Point3D point, Vector3D vector)
        {
            return Subtract(point, vector);
        }

        public Point3D Subtract(Vector3D vector)
        {
            return Subtract(this, vector);
        }

        public static Vector3D Subtract(Point3D point1, Point3D point2)
        {
            return new Vector3D(point1._x - point2._x, point1._y - point2._y, point1._z - point2._z);
        }

        public static Vector3D operator -(Point3D point1, Point3D point2)
        {
            return Subtract(point1, point2);
        }

        public Vector3D Subtract(Point3D other)
        {
            return Subtract(this, other);
        }
    }
}