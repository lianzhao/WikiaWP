namespace LianZhao.Media
{
    public partial struct Size3D
    {
        public static bool operator ==(Size3D size1, Size3D size2)
        {
            return Equals(size1, size2);
        }

        public static bool operator !=(Size3D size1, Size3D size2)
        {
            return !(size1 == size2);
        }

        public static bool Equals(Size3D size1, Size3D size2)
        {
            return size1._x == size2._x && size1._y == size2._y && size1._z == size2._z;
        }

        public bool Equals(Size3D other)
        {
            return Equals(this, other);
        }

        public override bool Equals(object o)
        {
            if (!(o is Size3D))
            {
                return false;
            }

            var value = (Size3D)o;
            return Equals(this, value);
        }

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode() ^ _z.GetHashCode();
        }
    }
}