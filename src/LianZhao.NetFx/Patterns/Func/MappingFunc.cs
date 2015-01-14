using System;

namespace LianZhao.Patterns.Func
{
    public static class MappingFunc
    {
        public static IFunc<T, T> ToFunc<T>(this IMappingFunc<T> mappingFunc)
        {
            return new DelegateFunc<T, T>(mappingFunc.ToDelegate());
        }

        public static Func<T, T> ToDelegate<T>(this IMappingFunc<T> mappingFunc)
        {
            return from =>
                {
                    var to = from;
                    return mappingFunc.TryInvoke(from, out to) ? to : from;
                };
        }
    }
}