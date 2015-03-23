namespace LianZhao.Media
{
    public partial struct Point3D
    {
        public static Point3D Add(Point3D point, Vector3D vector)
        {
            return new Point3D(point._x + vector._x, point._y + vector._y, point._z + vector._z);
        }

        public static Point3D operator +(Point3D point, Vector3D vector)
        {
            return Add(point, vector);
        }

        public Point3D Add(Vector3D vector)
        {
            return Add(this, vector);
        }
    }
}