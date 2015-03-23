using System;

namespace LianZhao.Media
{
    public partial struct Vector2D
    {
        private const string ConvertToStringSeperator = ",";

        public override string ToString()
        {
            return ConvertToString(null, null);
        }

        public string ToString(IFormatProvider provider)
        {
            return ConvertToString(null, provider);
        }

        string IFormattable.ToString(string format, IFormatProvider provider)
        {
            return ConvertToString(format, provider);
        }

        private string ConvertToString(string format, IFormatProvider provider)
        {
            return string.Format(provider, "x:{1}{0}y:{2}{0}z:{3}", ConvertToStringSeperator,
                _x.ToString(format, provider), _y.ToString(format, provider));
        }
    }
}