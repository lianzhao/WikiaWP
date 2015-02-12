using System;
using System.Threading.Tasks;

namespace LianZhao.Patterns.Func
{
    public static class AsyncAction
    {
        public static IAsyncAction<T> Empty<T>()
        {
            return EmptyAction<T>.Instance;
        }

        public static IAsyncAction<T> FromDelegate<T>(Action<T> action)
        {
            return new DelegateAction<T>(action);
        }

        public static IAsyncAction<T> FromDelegate<T>(Func<T, Task> func)
        {
            return new DelegateAsyncAction<T>(func);
        }
    }
}