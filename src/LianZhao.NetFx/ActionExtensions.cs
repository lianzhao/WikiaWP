using System;
using System.Threading.Tasks;

namespace LianZhao
{
    public static class ActionExtensions
    {
        public static Task InvokeAsync(this Action action, object state = null)
        {
            return Task.Factory.FromAsync(action.BeginInvoke, action.EndInvoke, state);
        }

        public static Task InvokeAsync<TArg>(this Action<TArg> action, TArg arg, object state)
        {
            return Task.Factory.FromAsync(action.BeginInvoke, action.EndInvoke, arg, state);
        }

        public static Task InvokeAsync<TArg1, TArg2>(
            this Action<TArg1, TArg2> action,
            TArg1 arg1,
            TArg2 arg2,
            object state = null)
        {
            return Task.Factory.FromAsync(action.BeginInvoke, action.EndInvoke, arg1, arg2, state);
        }

        public static Task InvokeAsync<TArg1, TArg2, TArg3>(
            this Action<TArg1, TArg2, TArg3> action,
            TArg1 arg1,
            TArg2 arg2,
            TArg3 arg3,
            object state = null)
        {
            return Task.Factory.FromAsync(action.BeginInvoke, action.EndInvoke, arg1, arg2, arg3, state);
        }
    }
}