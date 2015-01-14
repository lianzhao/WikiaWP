using System;

namespace LianZhao.Patterns.Func
{
    public static class Func
    {
        public static IFunc<T, T> Empty<T>()
        {
            return EmptyFunc<T>.Instance;
        }

        public static void Invoke<TFrom>(this IFunc<TFrom, Void> func, TFrom from)
        {
            func.Invoke(from);
        }

        public static TTo Invoke<TTo>(this IFunc<Void, TTo> func)
        {
            return func.Invoke(Void.Instance);
        }

        public static Func<TFrom, TTo> ToDelegate<TFrom, TTo>(this IFunc<TFrom, TTo> func)
        {
            return func.Invoke;
        }

        public static Action<TFrom> ToDelegate<TFrom>(this IFunc<TFrom, Void> func)
        {
            return from => func.Invoke(from);
        }

        public static EventHandler<TFrom> ToEventHandler<TFrom>(this IFunc<TFrom, Void> func)
        {
            return (obj, from) => func.Invoke(from);
        }

        public static Predicate<TFrom> ToPredicate<TFrom>(this IFunc<TFrom, bool> func)
        {
            return func.Invoke;
        }

        public static Func<TTo> ToDelegate<TTo>(this IFunc<Void, TTo> func)
        {
            return () => func.Invoke(Void.Instance);
        }

        public static IFunc<TFrom, TTo> FromDelegate<TFrom, TTo>(Func<TFrom, TTo> func)
        {
            return new DelegateFunc<TFrom, TTo>(func);
        }

        public static IFunc<TFrom, Void> FromDelegate<TFrom>(Action<TFrom> func)
        {
            return new DelegateFunc<TFrom, Void>(
                from =>
                    {
                        func.Invoke(from);
                        return Void.Instance;
                    });
        }

        public static IFunc<Void, TTo> FromDelegate<TTo>(Func<TTo> func)
        {
            return new DelegateFunc<Void, TTo>(_ => func.Invoke());
        }
    }
}