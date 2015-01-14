using System;

using MVVMSidekick.ViewModels;

namespace WikiaWP.Models
{

    //[DataContract(IsReference=true) ] //if you want
    public class PagingInfo_Model : BindableBase<PagingInfo_Model>
    {
        public PagingInfo_Model()
        {
            // Use propery to init value here:
            if (IsInDesignMode)
            {
                //Add design time demo data init here. These will not execute in runtime.
            }


        }

        //Use propvm + tab +tab  to create a new property of bindable here:


        public int TotalCount
        {
            get { return _TotalCountLocator(this).Value; }
            set { _TotalCountLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property int TotalCount Setup
        protected Property<int> _TotalCount = new Property<int> { LocatorFunc = _TotalCountLocator };
        static Func<BindableBase, ValueContainer<int>> _TotalCountLocator = RegisterContainerLocator<int>("TotalCount", model => model.Initialize("TotalCount", ref model._TotalCount, ref _TotalCountLocator, _TotalCountDefaultValueFactory));
        static Func<int> _TotalCountDefaultValueFactory = () => { return default(int); };
        #endregion

        public int PageCount
        {
            get { return _PageCountLocator(this).Value; }
            set { _PageCountLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property int PageCount Setup
        protected Property<int> _PageCount = new Property<int> { LocatorFunc = _PageCountLocator };
        static Func<BindableBase, ValueContainer<int>> _PageCountLocator = RegisterContainerLocator<int>("PageCount", model => model.Initialize("PageCount", ref model._PageCount, ref _PageCountLocator, _PageCountDefaultValueFactory));
        static Func<int> _PageCountDefaultValueFactory = () => { return default(int); };
        #endregion

        public int PageSize
        {
            get { return _PageSizeLocator(this).Value; }
            set { _PageSizeLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property int PageSize Setup
        protected Property<int> _PageSize = new Property<int> { LocatorFunc = _PageSizeLocator };
        static Func<BindableBase, ValueContainer<int>> _PageSizeLocator = RegisterContainerLocator<int>("PageSize", model => model.Initialize("PageSize", ref model._PageSize, ref _PageSizeLocator, _PageSizeDefaultValueFactory));
        static Func<int> _PageSizeDefaultValueFactory = () => { return default(int); };
        #endregion

        public int CurrentPage
        {
            get { return _CurrentPageLocator(this).Value; }
            set { _CurrentPageLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property int CurrentPage Setup
        protected Property<int> _CurrentPage = new Property<int> { LocatorFunc = _CurrentPageLocator };
        static Func<BindableBase, ValueContainer<int>> _CurrentPageLocator = RegisterContainerLocator<int>("CurrentPage", model => model.Initialize("CurrentPage", ref model._CurrentPage, ref _CurrentPageLocator, _CurrentPageDefaultValueFactory));
        static Func<int> _CurrentPageDefaultValueFactory = () => { return default(int); };
        #endregion

        public bool HasMore
        {
            get { return _HasMoreLocator(this).Value; }
            set { _HasMoreLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool HasMore Setup
        protected Property<bool> _HasMore = new Property<bool> { LocatorFunc = _HasMoreLocator };
        static Func<BindableBase, ValueContainer<bool>> _HasMoreLocator = RegisterContainerLocator<bool>("HasMore", model => model.Initialize("HasMore", ref model._HasMore, ref _HasMoreLocator, _HasMoreDefaultValueFactory));
        static Func<bool> _HasMoreDefaultValueFactory = () => { return default(bool); };
        #endregion

        public int LoadNextPageOffset
        {
            get { return _LoadNextPageOffsetLocator(this).Value; }
            set { _LoadNextPageOffsetLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property int LoadNextPageOffset Setup
        protected Property<int> _LoadNextPageOffset = new Property<int> { LocatorFunc = _LoadNextPageOffsetLocator };
        static Func<BindableBase, ValueContainer<int>> _LoadNextPageOffsetLocator = RegisterContainerLocator<int>("LoadNextPageOffset", model => model.Initialize("LoadNextPageOffset", ref model._LoadNextPageOffset, ref _LoadNextPageOffsetLocator, _LoadNextPageOffsetDefaultValueFactory));
        static Func<int> _LoadNextPageOffsetDefaultValueFactory = () => { return default(int); };
        #endregion

    }
}