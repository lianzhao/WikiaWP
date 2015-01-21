namespace LianZhao.Patterns.Func
{
    public interface ITryFunc<TFrom, TTo>
    {
        bool TryInvoke(TFrom from, out TTo to);
    }
}