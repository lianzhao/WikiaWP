using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using LianZhao.Patterns.Composite;

namespace LianZhao.Patterns.Func
{
    public class CompositeAsyncFunc<T> : CompositeObject<IAsyncFunc<T, T>>, IAsyncFunc<T, T>
    {
        public CompositeAsyncFunc()
        {
        }

        public CompositeAsyncFunc(IEnumerable<IAsyncFunc<T, T>> items)
            : base(items)
        {
        }

        public CompositeAsyncFunc(params IAsyncFunc<T, T>[] items)
            : base(items)
        {
        }

        public async Task<T> InvokeAsync(T from, CancellationToken cancellationToken = new CancellationToken())
        {
            var rv = from;
            await ForEachAsync(async fun => rv = await fun.InvokeAsync(rv, cancellationToken));
            return rv;
        }
    }
}