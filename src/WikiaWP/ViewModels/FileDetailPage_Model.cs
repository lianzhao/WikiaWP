using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace WikiaWP.ViewModels
{

    //[DataContract]
    public class FileDetailPage_Model : ViewModelBase<FileDetailPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性

        public FileDetailPage_Model()
        {
            if (IsInDesignMode)
            {
                Title = "Maelys Blackfyre TheMico.jpg";
                ImageSource =
                    "http://vignette3.wikia.nocookie.net/asoiaf/images/5/54/Maelys_Blackfyre_TheMico.jpg/revision/latest?path-prefix=zh";
                Content =
                    " 授权协议 该文件来自本维基的姊妹项目A wiki of ice and fire。 该文件可能受到版权保护，具体的版权信息，请至AWOIAF的（File:Maelys Blackfyre TheMico.jpg ）页面查询。 ";
            }
        }

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

        public string UploadInfo
        {
            get { return _UploadInfoLocator(this).Value; }
            set { _UploadInfoLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string UploadInfo Setup
        protected Property<string> _UploadInfo = new Property<string> { LocatorFunc = _UploadInfoLocator };
        static Func<BindableBase, ValueContainer<string>> _UploadInfoLocator = RegisterContainerLocator<string>("UploadInfo", model => model.Initialize("UploadInfo", ref model._UploadInfo, ref _UploadInfoLocator, _UploadInfoDefaultValueFactory));
        static Func<string> _UploadInfoDefaultValueFactory = () => { return default(string); };
        #endregion

        #region Life Time Event Handling

        ///// <summary>
        ///// This will be invoked by view when this viewmodel instance is set to view's ViewModel property. 
        ///// </summary>
        ///// <param name="view">Set target</param>
        ///// <param name="oldValue">Value before set.</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedToView(MVVMSidekick.Views.IView view, IViewModel oldValue)
        //{
        //    return base.OnBindedToView(view, oldValue);
        //}

        ///// <summary>
        ///// This will be invoked by view when this instance of viewmodel in ViewModel property is overwritten.
        ///// </summary>
        ///// <param name="view">Overwrite target view.</param>
        ///// <param name="newValue">The value replacing </param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnUnbindedFromView(MVVMSidekick.Views.IView view, IViewModel newValue)
        //{
        //    return base.OnUnbindedFromView(view, newValue);
        //}

        /// <summary>
        /// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        /// </summary>
        /// <param name="view">View that firing Load event</param>
        /// <returns>Task awaiter</returns>
        protected override async Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            await base.OnBindedViewLoad(view);

            if (string.IsNullOrEmpty(Title))
            {
                return;
            }

            using (var api = new ApiClient())
            {
                var article = await api.Wikia.Articles.GetArticleAsync(string.Format("File:{0}", Title), @abstract: 500);
                if (article == null)
                {
                    return;
                }
                ImageSource = article.OriginalImageSource;
                Content = article.@abstract;
                UploadInfo = article.revision == null
                                 ? null
                                 : string.Format(
                                     "由用户{0}于{1}上传",
                                     article.revision.user,
                                     article.revision.UploadedUtc.ToString());
            }
        }

        ///// <summary>
        ///// This will be invoked by view when the view fires Unload event and this viewmodel instance is still in view's  ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Unload event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        //{
        //    return base.OnBindedViewUnload(view);
        //}

        ///// <summary>
        ///// <para>If dispose actions got exceptions, will handled here. </para>
        ///// </summary>
        ///// <param name="exceptions">
        ///// <para>The exception and dispose infomation</para>
        ///// </param>
        //protected override async void OnDisposeExceptions(IList<DisposeInfo> exceptions)
        //{
        //    base.OnDisposeExceptions(exceptions);
        //    await TaskExHelper.Yield();
        //}

        #endregion



    }

}

