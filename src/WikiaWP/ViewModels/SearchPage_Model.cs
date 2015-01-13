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
using System.Windows;

using LianZhao.Collections.Generic;

using Microsoft.Phone.Wallet;

using Wikia;

namespace WikiaWP.ViewModels
{

    //[DataContract]
    public class SearchPage_Model : ViewModelBase<SearchPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性


        public string SearchText
        {
            get { return _SearchTextLocator(this).Value; }
            set { _SearchTextLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string SearchText Setup
        protected Property<string> _SearchText = new Property<string> { LocatorFunc = _SearchTextLocator };
        static Func<BindableBase, ValueContainer<string>> _SearchTextLocator = RegisterContainerLocator<string>("SearchText", model => model.Initialize("SearchText", ref model._SearchText, ref _SearchTextLocator, _SearchTextDefaultValueFactory));
        static Func<string> _SearchTextDefaultValueFactory = () => { return default(string); };
        #endregion

        public string MatchItemTitle
        {
            get { return _MatchItemTitleLocator(this).Value; }
            set { _MatchItemTitleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string MatchItemTitle Setup
        protected Property<string> _MatchItemTitle = new Property<string> { LocatorFunc = _MatchItemTitleLocator };
        static Func<BindableBase, ValueContainer<string>> _MatchItemTitleLocator = RegisterContainerLocator<string>("MatchItemTitle", model => model.Initialize("MatchItemTitle", ref model._MatchItemTitle, ref _MatchItemTitleLocator, _MatchItemTitleDefaultValueFactory));
        static Func<string> _MatchItemTitleDefaultValueFactory = () => { return default(string); };
        #endregion

        public string MatchItemContent
        {
            get { return _MatchItemContentLocator(this).Value; }
            set { _MatchItemContentLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string MatchItemContent Setup
        protected Property<string> _MatchItemContent = new Property<string> { LocatorFunc = _MatchItemContentLocator };
        static Func<BindableBase, ValueContainer<string>> _MatchItemContentLocator = RegisterContainerLocator<string>("MatchItemContent", model => model.Initialize("MatchItemContent", ref model._MatchItemContent, ref _MatchItemContentLocator, _MatchItemContentDefaultValueFactory));
        static Func<string> _MatchItemContentDefaultValueFactory = () => { return default(string); };
        #endregion

        public string MatchItemImageSource
        {
            get { return _MatchItemImageSourceLocator(this).Value; }
            set { _MatchItemImageSourceLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string MatchItemImageSource Setup
        protected Property<string> _MatchItemImageSource = new Property<string> { LocatorFunc = _MatchItemImageSourceLocator };
        static Func<BindableBase, ValueContainer<string>> _MatchItemImageSourceLocator = RegisterContainerLocator<string>("MatchItemImageSource", model => model.Initialize("MatchItemImageSource", ref model._MatchItemImageSource, ref _MatchItemImageSourceLocator, _MatchItemImageSourceDefaultValueFactory));
        static Func<string> _MatchItemImageSourceDefaultValueFactory = () => { return default(string); };
        #endregion

        public Visibility MatchItemPanelVisibility
        {
            get { return _MatchItemPanelVisibilityLocator(this).Value; }
            set { _MatchItemPanelVisibilityLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Visibility MatchItemPanelVisibility Setup
        protected Property<Visibility> _MatchItemPanelVisibility = new Property<Visibility> { LocatorFunc = _MatchItemPanelVisibilityLocator };
        static Func<BindableBase, ValueContainer<Visibility>> _MatchItemPanelVisibilityLocator = RegisterContainerLocator<Visibility>("MatchItemPanelVisibility", model => model.Initialize("MatchItemPanelVisibility", ref model._MatchItemPanelVisibility, ref _MatchItemPanelVisibilityLocator, _MatchItemPanelVisibilityDefaultValueFactory));
        static Func<Visibility> _MatchItemPanelVisibilityDefaultValueFactory = () => { return default(Visibility); };
        #endregion

        public Visibility SearchResultPanelVisibility
        {
            get { return _SearchResultPanelVisibilityLocator(this).Value; }
            set { _SearchResultPanelVisibilityLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Visibility SearchResultPanelVisibility Setup
        protected Property<Visibility> _SearchResultPanelVisibility = new Property<Visibility> { LocatorFunc = _SearchResultPanelVisibilityLocator };
        static Func<BindableBase, ValueContainer<Visibility>> _SearchResultPanelVisibilityLocator = RegisterContainerLocator<Visibility>("SearchResultPanelVisibility", model => model.Initialize("SearchResultPanelVisibility", ref model._SearchResultPanelVisibility, ref _SearchResultPanelVisibilityLocator, _SearchResultPanelVisibilityDefaultValueFactory));
        static Func<Visibility> _SearchResultPanelVisibilityDefaultValueFactory = () => { return default(Visibility); };
        #endregion


        public CommandModel<ReactiveCommand, String> CommandSearch
        {
            get { return _CommandSearchLocator(this).Value; }
            set { _CommandSearchLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandSearch Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandSearch = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandSearchLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandSearchLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandSearch", model => model.Initialize("CommandSearch", ref model._CommandSearch, ref _CommandSearchLocator, _CommandSearchDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandSearchDefaultValueFactory =
            model =>
            {
                var resource = "Search";           // Command resource  
                var commandId = "Search";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            vm.MatchItemPanelVisibility = Visibility.Collapsed;
                            vm.SearchResultPanelVisibility = Visibility.Collapsed;
                            if (string.IsNullOrWhiteSpace(vm.SearchText))
                            {
                                return;
                            }
                            string mapped = null;
                            var searchText = vm.SearchText;
                            using (var api = new ApiClient())
                            {
                                if (ApiClient.MainDictionary.TryGetValue(
                                    vm.SearchText,
                                    out mapped,
                                    StringComparison.OrdinalIgnoreCase)
                                    || ApiClient.RedirectDictionary.TryGetValue(
                                        vm.SearchText,
                                        out mapped,
                                        StringComparison.OrdinalIgnoreCase))
                                {
                                    searchText = mapped;
                                }
                                var article = await api.WikiaApi.Articles.GetArticleAsync(searchText, @abstract: 500);
                                if (article == null)
                                {
                                    //todo
                                    return;
                                }
                                vm.MatchItemTitle = article.title;
                                vm.MatchItemContent = article.@abstract;
                                vm.MatchItemImageSource = article.thumbnail;
                                vm.MatchItemPanelVisibility = Visibility.Visible;
                                vm.SearchResultPanelVisibility = Visibility.Collapsed;
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


        public CommandModel<ReactiveCommand, String> CommandLoadMore
        {
            get { return _CommandLoadMoreLocator(this).Value; }
            set { _CommandLoadMoreLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLoadMore Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandLoadMore = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLoadMoreLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoadMoreLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLoadMore", model => model.Initialize("CommandLoadMore", ref model._CommandLoadMore, ref _CommandLoadMoreLocator, _CommandLoadMoreDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoadMoreDefaultValueFactory =
            model =>
            {
                var resource = "LoadMore";           // Command resource  
                var commandId = "LoadMore";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            var tmp = vm.SearchText;
                            //Todo: Add LoadMore logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
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


        public CommandModel<ReactiveCommand, String> CommandNavigateToDetailPage
        {
            get { return _CommandNavigateToDetailPageLocator(this).Value; }
            set { _CommandNavigateToDetailPageLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandNavigateToDetailPage Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandNavigateToDetailPage = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandNavigateToDetailPageLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandNavigateToDetailPageLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandNavigateToDetailPage", model => model.Initialize("CommandNavigateToDetailPage", ref model._CommandNavigateToDetailPage, ref _CommandNavigateToDetailPageLocator, _CommandNavigateToDetailPageDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandNavigateToDetailPageDefaultValueFactory =
            model =>
            {
                var resource = "NavigateToDetailPage";           // Command resource  
                var commandId = "NavigateToDetailPage";
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .Subscribe(async _ =>
                        {
                            var vm = CastToCurrentType(model);
                            var vms = await
                                CastToCurrentType(model)
                                .StageManager
                                .DefaultStage
                                .ShowAndGetViewModel<ArticleDetailPage_Model>();
                            vms.ViewModel.Title = vm.MatchItemTitle;
                            await vms.Closing;
                        })
                    .DisposeWith(model);

                var cmdmdl = cmd.CreateCommandModel(resource);
                return cmdmdl;
            };
        #endregion

        public SearchPage_Model()
        {
            if (IsInDesignMode)
            {
                MatchItemTitle = "提利昂·兰尼斯特";
                MatchItemContent =
                    "提利昂·兰尼斯特（Tyrion Lannister）是泰温公爵和乔安娜夫人的第三个也是最小的孩子。因为是个侏儒，他有时候被戏称为小恶魔和半人。他利用自己的智慧屡次化险为夷，帮助兰尼斯特家族赢得了五王之战，但命运的不公使得他成为了一个弑亲者和通缉犯，踏上了流亡之路。 提利昂是书中一个非常重要的POV人物。在电视剧中，由皮特·丁拉基扮演。";
                MatchItemImageSource =
                    "http://vignette3.wikia.nocookie.net/asoiaf/images/0/04/Tyrion_Lannister_personal_arms.png/revision/latest/window-crop/width/200/x-offset/0/y-offset/0/window-width/400/window-height/400?cb=20120205044455&path-prefix=zh";
            }
        }

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

        ///// <summary>
        ///// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Load event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        //{
        //    return base.OnBindedViewLoad(view);
        //}

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

