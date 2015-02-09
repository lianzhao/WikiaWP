using System;

namespace LianZhao.Patterns.Func
{
    public class LazyFunc<TFrom, TTo> : IFunc<TFrom, TTo>
    {
        private readonly Lazy<IFunc<TFrom, TTo>> _lazy;

        public LazyFunc(Lazy<IFunc<TFrom, TTo>> lazy)
        {
            _lazy = lazy;
        }

        public TTo Invoke(TFrom @from)
        {
            return _lazy.Value.Invoke(from);
        }
    }
}