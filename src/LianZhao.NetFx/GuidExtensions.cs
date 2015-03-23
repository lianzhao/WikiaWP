using System;

namespace LianZhao
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid id)
        {
            return id.Equals(Guid.Empty);
        }
    }
}