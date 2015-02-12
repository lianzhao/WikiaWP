using System;
using System.Threading;
using System.Threading.Tasks;

namespace LianZhao.Patterns.Func
{
    public class DelegateAction<T> : IAction<T>, IAsyncAction<T>
    {
        private readonly Action<T> _action;

        public DelegateAction(Action<T> action)
        {
            _action = action;
        }

        public Void Invoke(T @from)
        {
            _action.Invoke(from);
            return Void.Instance;
        }

        public async Task<Void> InvokeAsync(T @from, CancellationToken cancellationToken = new CancellationToken())
        {
            await _action.InvokeAsync(from, null);
            return Void.Instance;
        }
    }
}