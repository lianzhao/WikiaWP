using System.Collections.Generic;

using LianZhao.Patterns.Composite;

namespace LianZhao.Patterns.Func
{
    public class CompositeFunc<T> : CompositeObject<IFunc<T, T>>, IFunc<T, T>
    {
        public CompositeFunc()
        {
        }

        public CompositeFunc(IEnumerable<IFunc<T, T>> items)
            : base(items)
        {
        }

        public CompositeFunc(params IFunc<T, T>[] items)
            : base(items)
        {
        }

        public T Invoke(T from)
        {
            var rv = from;
            ForEach(func => rv = func.Invoke(rv));
            return rv;
        }
    }
}