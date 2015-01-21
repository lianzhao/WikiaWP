using System.Collections.Generic;

using LianZhao.Patterns.Composite;

namespace LianZhao.Patterns.Func
{
    public class CompositeMappingFunc<T> : CompositeObject<IMappingFunc<T, T>>, IMappingFunc<T, T>
    {
        public CompositeMappingFunc()
        {
        }

        public CompositeMappingFunc(IEnumerable<IMappingFunc<T, T>> items)
            : base(items)
        {
        }

        public CompositeMappingFunc(params IMappingFunc<T, T>[] items)
            : base(items)
        {
        }

        public bool TryInvoke(T @from, out T to)
        {
            foreach (var func in Items)
            {
                if (func.TryInvoke(from, out to))
                {
                    return true;
                }
            }
            to = from;
            return false;
        }
    }
}