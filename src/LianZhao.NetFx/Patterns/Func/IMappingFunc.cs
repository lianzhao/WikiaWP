namespace LianZhao.Patterns.Func
{
    public interface IMappingFunc<T>
    {
        bool TryInvoke(T from, out T to);
    }
}