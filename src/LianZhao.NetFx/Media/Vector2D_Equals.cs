namespace LianZhao.Media
{
    public partial struct Vector2D
    {
        public static bool operator ==(Vector2D vector1, Vector2D vector2)
        {
            return Equals(vector1, vector2);
        }

        public static bool operator !=(Vector2D vector1, Vector2D vector2)
        {
            return !(vector1 == vector2);
        }

        public static bool Equals(Vector2D vector1, Vector2D vector2)
        {
            return vector1._x == vector2._x && vector1._y == vector2._y;
        }

        public bool Equals(Vector2D other)
        {
            return Equals(this, other);
        }

        public override bool Equals(object o)
        {
            if (!(o is Vector2D))
            {
                return false;
            }

            var value = (Vector2D)o;
            return Equals(this, value);
        }

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode();
        }
    }
}