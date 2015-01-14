using System;

namespace LianZhao
{
    public class ExceptionEventArgs : EventArgs
    {
        private bool _handled;

        public Exception Exception { get; private set; }

        public bool Handled
        {
            get
            {
                return _handled;
            }
            set
            {
                if (value)
                {
                    _handled = true;
                }
            }
        }

        public ExceptionEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}