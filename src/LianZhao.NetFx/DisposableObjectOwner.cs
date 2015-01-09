using System;

namespace LianZhao
{
    public class DisposableObjectOwner : DisposableObject
    {
        private readonly IDisposable _disposableObject;

        private readonly bool _isOwner;

        protected DisposableObjectOwner(IDisposable disposableObject, bool isOwner)
        {
            _disposableObject = disposableObject;
            _isOwner = isOwner;
        }

        protected override void ReleaseManagementResources()
        {
            base.ReleaseManagementResources();
            if (_isOwner)
            {
                _disposableObject.Dispose();
            }
        }
    }
}