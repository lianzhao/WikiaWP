using System;

namespace LianZhao
{
    public struct Void: IEquatable<Void>
    {
        public static readonly Void Instance = new Void();

        public bool Equals(Void other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is Void;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}