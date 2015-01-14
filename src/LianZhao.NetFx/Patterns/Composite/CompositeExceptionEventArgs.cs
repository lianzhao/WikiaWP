using System;

namespace LianZhao.Patterns.Composite
{
    public class CompositeExceptionEventArgs : ExceptionEventArgs
    {
        public int Index { get; private set; }

        public CompositeExceptionEventArgs(Exception exception, int index)
            : base(exception)
        {
            Index = index;
        }
    }
}