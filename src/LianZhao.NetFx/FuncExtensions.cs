using System;
using System.Threading.Tasks;

namespace LianZhao
{
    public static class FuncExtensions
    {
        public static Task<TResult> InvokeAsync<TResult>(this Func<TResult> func, object state = null)
        {
            return Task<TResult>.Factory.FromAsync(func.BeginInvoke, func.EndInvoke, state);
        }

        public static Task<TResult> InvokeAsync<TArg, TResult>(this Func<TArg, TResult> func, TArg arg, object state = null)
        {
            return Task<TResult>.Factory.FromAsync(func.BeginInvoke, func.EndInvoke, arg, state);
        }

        public static Task<TResult> InvokeAsync<TArg1, TArg2, TResult>(
            this Func<TArg1, TArg2, TResult> func,
            TArg1 arg1,
            TArg2 arg2,
            object state = null)
        {
            return Task<TResult>.Factory.FromAsync(func.BeginInvoke, func.EndInvoke, arg1, arg2, state);
        }

        public static Task<TResult> InvokeAsync<TArg1, TArg2, TArg3, TResult>(
            this Func<TArg1, TArg2, TArg3, TResult> func,
            TArg1 arg1,
            TArg2 arg2,
            TArg3 arg3,
            object state = null)
        {
            return Task<TResult>.Factory.FromAsync(func.BeginInvoke, func.EndInvoke, arg1, arg2, arg3, state);
        }

        public static bool TryHandle(this Func<Exception, bool> exceptionHandler, Exception ex)
        {
            return exceptionHandler != null && ex != null && exceptionHandler.Invoke(ex);
        }

        public static async Task<bool> TryHandleAsync(this Func<Exception, bool> exceptionHandler, Exception ex)
        {
            return exceptionHandler != null && ex != null && await exceptionHandler.InvokeAsync(ex);
        }
    }
}