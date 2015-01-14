using System;
using System.Threading;
using System.Threading.Tasks;

namespace LianZhao.Patterns.Func
{
    public class DelegateFunc<TFrom, TTo> : IFunc<TFrom, TTo>, IAsyncFunc<TFrom, TTo>
    {
        private readonly Func<TFrom, TTo> _func;

        public DelegateFunc(Func<TFrom, TTo> func)
        {
            _func = func;
        }

        public TTo Invoke(TFrom from)
        {
            return _func.Invoke(from);
        }

        public Task<TTo> InvokeAsync(TFrom from, CancellationToken cancellationToken = new CancellationToken())
        {
            return _func.InvokeAsync(from);
        }
    }
}