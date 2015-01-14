using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LianZhao.Patterns.Composite
{
    public class CompositeObject<T> : IComposite<T>
    {
        public ICollection<T> Items { get; private set; }

        public CompositeObject()
        {
            Items = new List<T>();
        }

        public CompositeObject(IEnumerable<T> items)
        {
            Items = new List<T>(items);
        }

        public CompositeObject(params T[] items)
            : this(items as IEnumerable<T>)
        {
        }

        protected void ForEach(Action<T> action)
        {
            ForEach(
                (item, i) =>
                    {
                        action.Invoke(item);
                        return true;
                    });
        }

        protected async Task ForEachAsync(Func<T, Task> func)
        {
            await ForEachAsync(
                async (item, i) =>
                    {
                        await func.Invoke(item);
                        return true;
                    });
        }

        protected void ForEach(Action<T, int> action)
        {
            ForEach(
                (item, i) =>
                    {
                        action.Invoke(item, i);
                        return true;
                    });
        }

        protected async Task ForEachAsync(Func<T, int, Task> func)
        {
            await ForEachAsync(
                async (item, i) =>
                    {
                        await func.Invoke(item, i);
                        return true;
                    });
        }

        protected void ForEach(Func<T, bool> func)
        {
            ForEach((item, i) => func.Invoke(item));
        }

        protected async Task ForEachAsync(Func<T, Task<bool>> func)
        {
            await ForEachAsync(async (item, i) => await func.Invoke(item));
        }

        protected virtual void ForEach(Func<T, int, bool> func)
        {
            var i = 0;
            foreach (var item in Items)
            {
                try
                {
                    var shouldContinue = func.Invoke(item, i);
                    if (!shouldContinue)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    var args = new CompositeExceptionEventArgs(ex, i);
                    OnFaulted(args);
                    if (!args.Handled)
                    {
                        throw;
                    }
                }
                finally
                {
                    i++;
                }
            }
        }

        protected virtual async Task ForEachAsync(Func<T, int, Task<bool>> func)
        {
            var i = 0;
            foreach (var item in Items)
            {
                try
                {
                    var shouldContinue = await func.Invoke(item, i);
                    if (!shouldContinue)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    var args = new CompositeExceptionEventArgs(ex, i);
                    OnFaulted(args);
                    if (!args.Handled)
                    {
                        throw;
                    }
                }
                finally
                {
                    i++;
                }
            }
        }

        public event EventHandler<CompositeExceptionEventArgs> Faulted;

        protected virtual void OnFaulted(CompositeExceptionEventArgs e)
        {
            var handler = Faulted;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}