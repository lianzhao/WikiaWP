using System.Collections.Generic;

using LianZhao.Patterns.Composite;

namespace LianZhao.Patterns.Func
{
    public class CompositeAction<T> : CompositeObject<IAction<T>>, IAction<T>
    {
        public CompositeAction()
        {
        }

        public CompositeAction(IEnumerable<IAction<T>> items)
            : base(items)
        {
        }

        public CompositeAction(params IAction<T>[] items)
            : base(items)
        {
        }

        public Void Invoke(T from)
        {
            ForEach(action => action.Invoke(from));
            return Void.Instance;
        }
    }
}