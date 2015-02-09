using System;
using System.Threading;
using System.Threading.Tasks;

namespace LianZhao.Patterns.Func
{
    public class LazyAsyncFunc<TFrom, TTo> : IAsyncFunc<TFrom, TTo>
    {
        private readonly Lazy<IAsyncFunc<TFrom, TTo>> _lazy;

        public LazyAsyncFunc(Lazy<IAsyncFunc<TFrom, TTo>> lazy)
        {
            _lazy = lazy;
        }

        public Task<TTo> InvokeAsync(TFrom @from, CancellationToken cancellationToken = new CancellationToken())
        {
            return _lazy.Value.InvokeAsync(from, cancellationToken);
        }
    }
}