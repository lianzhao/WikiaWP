namespace LianZhao.Media
{
    public partial struct Size2D
    {
        public static bool operator ==(Size2D size1, Size2D size2)
        {
            return Equals(size1, size2);
        }

        public static bool operator !=(Size2D size1, Size2D size2)
        {
            return !(size1 == size2);
        }

        public static bool Equals(Size2D size1, Size2D size2)
        {
            return size1._width == size2._width && size1._height == size2._height;
        }

        public bool Equals(Size2D other)
        {
            return Equals(this, other);
        }

        public override bool Equals(object o)
        {
            if (!(o is Size2D))
            {
                return false;
            }

            var value = (Size2D)o;
            return Equals(this, value);
        }

        public override int GetHashCode()
        {
            return _width.GetHashCode() ^ _height.GetHashCode();
        }
    }
}