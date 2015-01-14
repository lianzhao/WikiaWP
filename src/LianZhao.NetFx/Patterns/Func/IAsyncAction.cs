namespace LianZhao.Patterns.Func
{
    public interface IAsyncAction<in T> : IAsyncFunc<T, Void>
    {
    }
}