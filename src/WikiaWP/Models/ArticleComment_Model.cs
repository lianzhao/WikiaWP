using System;

using MVVMSidekick.ViewModels;

namespace WikiaWP.Models
{
    //[DataContract(IsReference=true) ] //if you want
    public class ArticleComment_Model : BindableBase<ArticleComment_Model>
    {
        public const string DefaultUserName = "一个匿名参与者";

        public ArticleComment_Model()
        {
            // Use propery to init value here:
            if (IsInDesignMode)
            {
                //Add design time demo data init here. These will not execute in runtime.
            }


        }

        //Use propvm + tab +tab  to create a new property of bindable here:


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

        public DateTime CreateDateTimeUtc
        {
            get { return _CreateDateTimeUtcLocator(this).Value; }
            set { _CreateDateTimeUtcLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property DateTime CreateDateTimeUtc Setup
        protected Property<DateTime> _CreateDateTimeUtc = new Property<DateTime> { LocatorFunc = _CreateDateTimeUtcLocator };
        static Func<BindableBase, ValueContainer<DateTime>> _CreateDateTimeUtcLocator = RegisterContainerLocator<DateTime>("CreateDateTimeUtc", model => model.Initialize("CreateDateTimeUtc", ref model._CreateDateTimeUtc, ref _CreateDateTimeUtcLocator, _CreateDateTimeUtcDefaultValueFactory));
        static Func<DateTime> _CreateDateTimeUtcDefaultValueFactory = () => { return default(DateTime); };
        #endregion

        public string CreateUser
        {
            get { return _CreateUserLocator(this).Value; }
            set { _CreateUserLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string CreateUser Setup
        protected Property<string> _CreateUser = new Property<string> { LocatorFunc = _CreateUserLocator };
        static Func<BindableBase, ValueContainer<string>> _CreateUserLocator = RegisterContainerLocator<string>("CreateUser", model => model.Initialize("CreateUser", ref model._CreateUser, ref _CreateUserLocator, _CreateUserDefaultValueFactory));
        static Func<string> _CreateUserDefaultValueFactory = () => { return default(string); };
        #endregion

    }
}