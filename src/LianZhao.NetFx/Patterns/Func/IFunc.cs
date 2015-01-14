namespace LianZhao.Patterns.Func
{
    public interface IFunc<in TFrom, out TTo>
    {
        TTo Invoke(TFrom from);
    }
}