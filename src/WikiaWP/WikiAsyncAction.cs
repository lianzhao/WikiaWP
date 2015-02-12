using System;
using System.Net;

using LianZhao.Patterns.Func;

using Void = LianZhao.Void;

namespace WikiaWP
{
    public class WikiAsyncAction : WikiAsyncFunc<Void>
    {
        public WikiAsyncAction(
            IAsyncFunc<ApiClient, Void> func,
            Action<UnhandledErrorEventArgs<Exception>> onError = null,
            Action<bool> onFinally = null)
            : base(func, onError, onFinally)
        {
        }
    }
}