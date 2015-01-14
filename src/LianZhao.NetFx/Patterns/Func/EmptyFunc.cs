using System.Threading;
using System.Threading.Tasks;

namespace LianZhao.Patterns.Func
{
    public class EmptyFunc<T> : IFunc<T, T>, IAsyncFunc<T, T>
    {
        public static readonly EmptyFunc<T> Instance = new EmptyFunc<T>();

        private EmptyFunc()
        {
        }

        public T Invoke(T from)
        {
            return from;
        }

        public Task<T> InvokeAsync(T from, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(from);
        }
    }
}