using System;

namespace LianZhao.Media
{
    public partial struct Size2D
    {
        internal readonly double _width;

        internal readonly double _height;

        public static readonly Size2D Empty = new Size2D();

        public Size2D(double width, double height)
        {
            if (width < 0 || height < 0)
            {
                throw new ArgumentException("Width and height can not be negative");
            }

            _width = width;
            _height = height;
        }

        public double Width
        {
            get { return _width; }
        }

        public double Height
        {
            get { return _height; }
        }

        public bool IsEmpty
        {
            get { return _width == 0 && _height == 0; }
        }

        public double Size
        {
            get { return _width*_height; }
        }
    }
}