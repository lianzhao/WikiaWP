using System;

namespace LianZhao
{
    public class DisposableObject : IDisposable
    {
        public bool IsDisposed { get; private set; }

        protected DisposableObject()
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool fromDispose)
        {
            if (IsDisposed)
            {
                return;
            }

            if (fromDispose)
            {
                ReleaseManagementResources();
            }
            ReleaseNativeResources();
            IsDisposed = true;
        }

        protected virtual void ReleaseManagementResources()
        {
            
        }

        protected virtual void ReleaseNativeResources()
        {

        }

        protected void ThrowIfDisposed(string objectName = null)
        {
            if (!IsDisposed)
            {
                return;
            }

            objectName = objectName ?? GetType().FullName;
            throw new ObjectDisposedException(objectName);
        }

        ~DisposableObject()
        {
            Dispose(false);
        }
    }
}