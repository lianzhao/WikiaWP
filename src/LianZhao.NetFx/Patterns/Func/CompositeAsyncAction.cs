using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LianZhao.Patterns.Composite;

namespace LianZhao.Patterns.Func
{

    public class CompositeAsyncAction<T> : CompositeObject<IAsyncAction<T>>, IAsyncAction<T>
    {
        public bool ParallelInvoke { get; set; }

        public CompositeAsyncAction()
        {
        }

        public CompositeAsyncAction(IEnumerable<IAsyncAction<T>> items)
            : base(items)
        {
        }

        public CompositeAsyncAction(params IAsyncAction<T>[] items)
            : base(items)
        {
        }

        public async Task<Void> InvokeAsync(T from, CancellationToken cancellationToken = new CancellationToken())
        {
            if (ParallelInvoke)
            {
                await Task.WhenAll(Items.Select(item => item.InvokeAsync(from, cancellationToken)));
            }
            else
            {
                await ForEachAsync(async func => await func.InvokeAsync(from, cancellationToken));
            }

            return Void.Instance;
        }
    }
}