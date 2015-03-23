namespace LianZhao.Media
{
    public partial struct Vector3D
    {
        public static Vector3D Divide(Vector3D vector, double scalar)
        {
            return Multiply(vector, 1.0/scalar);
        }

        public static Vector3D operator /(Vector3D vector, double scalar)
        {
            return Divide(vector, scalar);
        }

        public Vector3D Divide(double scalar)
        {
            return Divide(this, scalar);
        }
    }
}