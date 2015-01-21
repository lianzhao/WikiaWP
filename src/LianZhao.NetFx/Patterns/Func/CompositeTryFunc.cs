using System.Collections.Generic;

using LianZhao.Patterns.Composite;

namespace LianZhao.Patterns.Func
{
    public class CompositeTryFunc<T> : CompositeObject<ITryFunc<T, T>>, ITryFunc<T, T>
    {
        public CompositeTryFunc()
        {
        }

        public CompositeTryFunc(IEnumerable<ITryFunc<T, T>> items)
            : base(items)
        {
        }

        public CompositeTryFunc(params ITryFunc<T, T>[] items)
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