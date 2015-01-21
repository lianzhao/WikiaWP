namespace LianZhao.Patterns.Func
{
    public interface IMappingFunc<TFrom, TTo>
    {
        bool TryInvoke(TFrom from, out TTo to);
    }
}