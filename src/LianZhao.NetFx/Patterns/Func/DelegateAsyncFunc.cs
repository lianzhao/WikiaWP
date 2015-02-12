using System;
using System.Threading;
using System.Threading.Tasks;

namespace LianZhao.Patterns.Func
{
    public class DelegateAsyncFunc<TFrom, TTo> : IAsyncFunc<TFrom, TTo>
    {
        private readonly Func<TFrom, Task<TTo>> _func;

        public DelegateAsyncFunc(Func<TFrom, Task<TTo>> func)
        {
            _func = func;
        }

        public async Task<TTo> InvokeAsync(TFrom @from, CancellationToken cancellationToken = new CancellationToken())
        {
            return await _func.Invoke(from);
        }
    }
}