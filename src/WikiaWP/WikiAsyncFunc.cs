using System;
using System.Net;
using System.Threading.Tasks;

using LianZhao.Patterns.Func;

using Microsoft.Phone.Reactive;

using Void = LianZhao.Void;

namespace WikiaWP
{
    public class WikiAsyncFunc<TResult>
    {
        private readonly IAsyncFunc<ApiClient, TResult> _func;

        private Action<UnhandledErrorEventArgs<Exception>> _onError;

        private Action<bool> _onFinally;

        public WikiAsyncFunc(
            IAsyncFunc<ApiClient, TResult> func,
            Action<UnhandledErrorEventArgs<Exception>> onError = null,
            Action<bool> onFinally = null)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            _func = func;
            _onError = onError;
            _onFinally = onFinally;
        }

        public async Task<TResult> InvokeAsync()
        {
            var success = false;
            try
            {
                TResult result;
                using (var api = new ApiClient())
                {
                    result = await _func.InvokeAsync(api);
                }
                success = true;
                return result;
            }
            catch (Exception ex)
            {
                if (_onError == null)
                {
                    throw;
                }
                var e = new UnhandledErrorEventArgs<Exception>(ex);
                _onError(e);
                if (e.Handled)
                {
                    return e.Result;
                }
                throw;
            }
            finally
            {
                if (_onFinally != null)
                {
                    _onFinally(success);
                }
            }
        }

        public WikiAsyncFunc<TResult> OnError(Action<UnhandledErrorEventArgs<Exception>> handler)
        {
            _onError += handler;
            return this;
        }

        public WikiAsyncFunc<TResult> OnFinally(Action<bool> handler)
        {
            _onFinally += handler;
            return this;
        }

        public class UnhandledErrorEventArgs<TError> : EventArgs
        {
            public TError Error { get; private set; }

            public bool Handled { get; private set; }

            public TResult Result { get; private set; }

            public UnhandledErrorEventArgs(TError error)
            {
                Error = error;
            }

            public void SetHandled(TResult result = default (TResult))
            {
                Handled = true;
                Result = result;
            }
        }
    }

    public static class WikiAsyncFunc
    {
        public static WikiAsyncFunc<TResult> Create<TResult>(
            IAsyncFunc<ApiClient, TResult> func,
            Action<WikiAsyncFunc<TResult>.UnhandledErrorEventArgs<Exception>> onError = null,
            Action<bool> onFinally = null)
        {
            return new WikiAsyncFunc<TResult>(func, onError, onFinally);
        }

        public static WikiAsyncFunc<TResult> Create<TResult>(
            Func<ApiClient, Task<TResult>> func,
            Action<WikiAsyncFunc<TResult>.UnhandledErrorEventArgs<Exception>> onError = null,
            Action<bool> onFinally = null)
        {
            return new WikiAsyncFunc<TResult>(AsyncFunc.FromDelegate(func), onError, onFinally);
        }

        public static WikiAsyncFunc<Void> Create(
            IAsyncFunc<ApiClient, Void> func,
            Action<WikiAsyncFunc<Void>.UnhandledErrorEventArgs<Exception>> onError = null,
            Action<bool> onFinally = null)
        {
            return new WikiAsyncFunc<Void>(func, onError, onFinally);
        }

        public static WikiAsyncFunc<Void> Create(
            Func<ApiClient, Task> func,
            Action<WikiAsyncFunc<Void>.UnhandledErrorEventArgs<Exception>> onError = null,
            Action<bool> onFinally = null)
        {
            return new WikiAsyncFunc<Void>(AsyncAction.FromDelegate(func), onError, onFinally);
        }

        public static WikiAsyncFunc<TResult> OnHttpError<TResult>(
            this WikiAsyncFunc<TResult> wikiFunc,
            Action<WikiAsyncFunc<TResult>.UnhandledErrorEventArgs<HttpStatusCode>> handler)
        {
            Action<WikiAsyncFunc<TResult>.UnhandledErrorEventArgs<Exception>> errorHandler = e =>
                {
                    if (e.Handled)
                    {
                        return;
                    }
                    var httpStatusCodeProvider = e.Error as IFunc<LianZhao.Void, HttpStatusCode>;
                    if (httpStatusCodeProvider == null)
                    {
                        return;
                    }

                    var args =
                        new WikiAsyncFunc<TResult>.UnhandledErrorEventArgs<HttpStatusCode>(
                            httpStatusCodeProvider.Invoke());
                    handler.Invoke(args);
                    if (args.Handled)
                    {
                        e.SetHandled(args.Result);
                    }
                };
            return wikiFunc.OnError(errorHandler);
        }

        public static WikiAsyncFunc<TResult> IgnoreHttpError<TResult>(
            this WikiAsyncFunc<TResult> wikiFunc,
            HttpStatusCode error,
            Func<TResult> func)
        {
            Action<WikiAsyncFunc<TResult>.UnhandledErrorEventArgs<HttpStatusCode>> errorHandler = e =>
                {
                    if (e.Handled)
                    {
                        return;
                    }
                    if (error == e.Error)
                    {
                        e.SetHandled(func.Invoke());
                    }
                };
            return wikiFunc.OnHttpError(errorHandler);
        }

        public static WikiAsyncFunc<TResult> IgnoreAllError<TResult>(
            this WikiAsyncFunc<TResult> wikiFunc,
            Func<TResult> func)
        {
            Action<WikiAsyncFunc<TResult>.UnhandledErrorEventArgs<Exception>> errorHandler = e =>
                {
                    if (e.Handled)
                    {
                        return;
                    }
                    e.SetHandled(func.Invoke());
                };
            return wikiFunc.OnError(errorHandler);
        }

        public static WikiAsyncFunc<Void> IgnoreHttpError(
            this WikiAsyncFunc<Void> wikiFunc,
            HttpStatusCode error,
            System.Action action)
        {
            Action<WikiAsyncFunc<Void>.UnhandledErrorEventArgs<HttpStatusCode>> errorHandler = e =>
                {
                    if (e.Handled)
                    {
                        return;
                    }
                    if (error == e.Error)
                    {
                        action.Invoke();
                        e.SetHandled();
                    }
                };
            return wikiFunc.OnHttpError(errorHandler);
        }

        public static WikiAsyncFunc<Void> IgnoreError(
            this WikiAsyncFunc<Void> wikiFunc,
            Action<Exception> action = null)
        {
            Action<WikiAsyncFunc<Void>.UnhandledErrorEventArgs<Exception>> errorHandler = e =>
                {
                    if (e.Handled)
                    {
                        return;
                    }
                    if (action != null)
                    {
                        action.Invoke(e.Error);
                    }
                    e.SetHandled(Void.Instance);
                };
            return wikiFunc.OnError(errorHandler);
        }
    }
}