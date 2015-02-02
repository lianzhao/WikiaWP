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

        public CommandModel<ReactiveCommand, String> CommandLoadImageInfo
        {
            get { return _CommandLoadImageInfoLocator(this).Value; }
            set { _CommandLoadImageInfoLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLoadImageInfo Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandLoadImageInfo = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLoadImageInfoLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoadImageInfoLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLoadImageInfo", model => model.Initialize("CommandLoadImageInfo", ref model._CommandLoadImageInfo, ref _CommandLoadImageInfoLocator, _CommandLoadImageInfoDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoadImageInfoDefaultValueFactory =
            model =>
            {
                var resource = "LoadImageInfo";           // Command resource  
                var commandId = "LoadImageInfo";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            if (string.IsNullOrEmpty(vm.Title))
                            {
                                return;
                            }

                            using (var api = new ApiClient())
                            {
                                var article = await api.Wikia.Articles.GetArticleAsync(vm.Title, @abstract: 500);
                                vm.ImageSource = article.OriginalImageSource;
                            }
                        }
                    )
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
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
                ImageSource = article.OriginalImageSource;
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

