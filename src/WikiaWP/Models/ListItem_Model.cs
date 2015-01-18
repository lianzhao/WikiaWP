using System;

using MVVMSidekick.ViewModels;

namespace WikiaWP.Models
{
    //[DataContract(IsReference=true) ] //if you want
    public class ListItem_Model : BindableBase<ListItem_Model>
    {
        public ListItem_Model()
        {
            // Use propery to init value here:
            if (IsInDesignMode)
            {
                //Add design time demo data init here. These will not execute in runtime.
            }


        }

        //Use propvm + tab +tab  to create a new property of bindable here:

        
        public string Title
        {
            get { return _TitleLocator(this).Value; }
            set { _TitleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Title Setup
        protected Property<string> _Title = new Property<string> { LocatorFunc = _TitleLocator };
        static Func<BindableBase, ValueContainer<string>> _TitleLocator = RegisterContainerLocator<string>("Title", model => model.Initialize("Title", ref model._Title, ref _TitleLocator, _TitleDefaultValueFactory));
        static Func<string> _TitleDefaultValueFactory = () => { return default(string); };
        #endregion

        public string SubTitle
        {
            get { return _SubTitleLocator(this).Value; }
            set { _SubTitleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string SubTitle Setup
        protected Property<string> _SubTitle = new Property<string> { LocatorFunc = _SubTitleLocator };
        static Func<BindableBase, ValueContainer<string>> _SubTitleLocator = RegisterContainerLocator<string>("SubTitle", model => model.Initialize("SubTitle", ref model._SubTitle, ref _SubTitleLocator, _SubTitleDefaultValueFactory));
        static Func<string> _SubTitleDefaultValueFactory = () => { return default(string); };
        #endregion

        public string Content
        {
            get { return _ContentLocator(this).Value; }
            set { _ContentLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Content Setup
        protected Property<string> _Content = new Property<string> { LocatorFunc = _ContentLocator };
        static Func<BindableBase, ValueContainer<string>> _ContentLocator = RegisterContainerLocator<string>("Content", model => model.Initialize("Content", ref model._Content, ref _ContentLocator, _ContentDefaultValueFactory));
        static Func<string> _ContentDefaultValueFactory = () => { return default(string); };
        #endregion

        public string ImageSource
        {
            get { return _ImageSourceLocator(this).Value; }
            set { _ImageSourceLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string ImageSource Setup
        protected Property<string> _ImageSource = new Property<string> { LocatorFunc = _ImageSourceLocator };
        static Func<BindableBase, ValueContainer<string>> _ImageSourceLocator = RegisterContainerLocator<string>("ImageSource", model => model.Initialize("ImageSource", ref model._ImageSource, ref _ImageSourceLocator, _ImageSourceDefaultValueFactory));
        static Func<string> _ImageSourceDefaultValueFactory = () => { return default(string); };
        #endregion

        public string Group
        {
            get { return _GroupLocator(this).Value; }
            set { _GroupLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Group Setup
        protected Property<string> _Group = new Property<string> { LocatorFunc = _GroupLocator };
        static Func<BindableBase, ValueContainer<string>> _GroupLocator = RegisterContainerLocator<string>("Group", model => model.Initialize("Group", ref model._Group, ref _GroupLocator, _GroupDefaultValueFactory));
        static Func<string> _GroupDefaultValueFactory = () => { return default(string); };
        #endregion

    }
}