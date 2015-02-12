using System;
using System.Threading;
using System.Threading.Tasks;

namespace LianZhao.Patterns.Func
{
    public class DelegateAsyncAction<T> : IAsyncAction<T>
    {
        private readonly Func<T, Task> _func;

        public DelegateAsyncAction(Func<T, Task> func)
        {
            _func = func;
        }

        public async Task<Void> InvokeAsync(T @from, CancellationToken cancellationToken = new CancellationToken())
        {
            await _func.Invoke(from);
            return Void.Instance;
        }
    }
}