using System.Threading;
using System.Threading.Tasks;

namespace LianZhao.Patterns.Func
{
    public class EmptyAction<T> : IAction<T>, IAsyncAction<T>
    {
        public static readonly EmptyAction<T> Instance = new EmptyAction<T>();

        private EmptyAction()
        {
        }

        public Void Invoke(T from)
        {
            return Void.Instance;
        }

        public Task<Void> InvokeAsync(T from, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(Void.Instance);
        }
    }
}