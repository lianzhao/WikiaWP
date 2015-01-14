using System.Collections.Generic;

namespace LianZhao.Patterns.Composite
{
    public interface IComposite<T>
    {
        ICollection<T> Items { get; }
    }
}