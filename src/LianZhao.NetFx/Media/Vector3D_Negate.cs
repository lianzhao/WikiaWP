namespace LianZhao.Media
{
    public partial struct Vector3D
    {
        public static Vector3D Nagate(Vector3D vector)
        {
            return new Vector3D(-vector._x, -vector._y, -vector._z);
        }

        public static Vector3D operator -(Vector3D vector)
        {
            return Nagate(vector);
        }

        public Vector3D Nagate()
        {
            return Nagate(this);
        }
    }
}