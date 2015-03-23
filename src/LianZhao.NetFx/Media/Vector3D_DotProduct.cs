namespace LianZhao.Media
{
    public partial struct Vector3D
    {
        public static double DotProduct(Vector3D vector1, Vector3D vector2)
        {
            return vector1._x*vector2._x + vector1._y*vector2._y + vector1._z*vector2._z;
        }

        public double DotProduct(Vector3D other)
        {
            return DotProduct(this, other);
        }
    }
}