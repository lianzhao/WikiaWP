namespace LianZhao.Media
{
    public partial struct Vector3D
    {
        public static Vector3D Multiply(Vector3D vector, double scalar)
        {
            return new Vector3D(vector._x*scalar, vector._y*scalar, vector._z*scalar);
        }

        public static Vector3D operator *(Vector3D vector, double scalar)
        {
            return Multiply(vector, scalar);
        }

        public static Vector3D operator *(double scalar, Vector3D vector)
        {
            return Multiply(vector, scalar);
        }
    }
}