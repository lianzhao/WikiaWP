using System;
using System.Threading;
using System.Threading.Tasks;

namespace LianZhao.Patterns.Func
{
    public static class AsyncFunc
    {
        public static IAsyncFunc<T, T> Empty<T>()
        {
            return EmptyFunc<T>.Instance;
        }

        public static async Task InvokeAsync<TFrom>(
            this IAsyncFunc<TFrom, Void> func,
            TFrom from,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await func.InvokeAsync(from, cancellationToken);
        }

        public static async Task<TTo> InvokeAsync<TTo>(
            this IAsyncFunc<Void, TTo> func,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await func.InvokeAsync(Void.Instance, cancellationToken);
        }

        public static IAsyncFunc<TFrom, TTo> FromDelegate<TFrom, TTo>(Func<TFrom, TTo> func)
        {
            return new DelegateFunc<TFrom, TTo>(func);
        }

        public static IAsyncFunc<TFrom, Void> FromDelegate<TFrom>(Action<TFrom> func)
        {
            return new DelegateFunc<TFrom, Void>(
                from =>
                {
                    func.Invoke(from);
                    return Void.Instance;
                });
        }

        public static IAsyncFunc<Void, TTo> FromDelegate<TTo>(Func<TTo> func)
        {
            return new DelegateFunc<Void, TTo>(_ => func.Invoke());
        }

        public static IAsyncFunc<TFrom, TTo> FromDelegate<TFrom, TTo>(Func<TFrom, Task<TTo>> func)
        {
            return new DelegateAsyncFunc<TFrom, TTo>(func);
        }
    }
}