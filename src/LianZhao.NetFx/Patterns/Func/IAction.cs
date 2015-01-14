namespace LianZhao.Patterns.Func
{
    public interface IAction<in T> : IFunc<T, Void>
    {
    }
}