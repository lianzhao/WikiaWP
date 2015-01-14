using System.Threading;
using System.Threading.Tasks;

namespace LianZhao.Patterns.Func
{
    public interface IAsyncFunc<in TFrom, TTo>
    {
        Task<TTo> InvokeAsync(TFrom from, CancellationToken cancellationToken = default(CancellationToken));
    }
}