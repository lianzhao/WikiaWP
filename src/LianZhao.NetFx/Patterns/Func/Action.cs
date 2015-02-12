using System;

namespace LianZhao.Patterns.Func
{
    public static class Action
    {
        public static IAction<T> Empty<T>()
        {
            return EmptyAction<T>.Instance;
        }

        public static IAction<T> FromDelegate<T>(Action<T> action)
        {
            return new DelegateAction<T>(action);
        }

        public static IAction<Void> FromDelegate(System.Action action)
        {
            return new DelegateAction<Void>(_ => action.Invoke());
        }
    }
}