namespace LianZhao.Media
{
    public partial struct Vector3D
    {
        public static Vector3D CrossProduct(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(vector1._y*vector2._z - vector1._z*vector2._y,
                vector1._z*vector2._x - vector1._x*vector2._z, vector1._x*vector2._y - vector1._y*vector2._x);
        }

        public Vector3D CrossProduct(Vector3D other)
        {
            return CrossProduct(this, other);
        }
    }
}