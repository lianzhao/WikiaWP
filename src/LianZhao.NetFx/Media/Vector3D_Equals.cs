namespace LianZhao.Media
{
    public partial struct Vector3D
    {
        public static bool operator ==(Vector3D vector1, Vector3D vector2)
        {
            return Equals(vector1, vector2);
        }

        public static bool operator !=(Vector3D vector1, Vector3D vector2)
        {
            return !(vector1 == vector2);
        }

        public static bool Equals(Vector3D vector1, Vector3D vector2)
        {
            return vector1._x == vector2._x && vector1._y == vector2._y && vector1._z == vector2._z;
        }

        public bool Equals(Vector3D other)
        {
            return Equals(this, other);
        }

        public override bool Equals(object o)
        {
            if (!(o is Vector3D))
            {
                return false;
            }

            var value = (Vector3D)o;
            return Equals(this, value);
        }

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode() ^ _z.GetHashCode();
        }
    }
}