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
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows;

using Windows.Phone.Management.Deployment;

using LianZhao.Collections.Generic;

using Microsoft.Phone.Wallet;

using Wikia;

using WikiaWP.Models;
using WikiaWP.Resources;

using ZhAsoiafWiki.Plus;
using ZhAsoiafWiki.Plus.Models;

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

        public ListItem_Model MatchItem
        {
            get { return _MatchItemLocator(this).Value; }
            set { _MatchItemLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ListItem_Model MatchItem Setup
        protected Property<ListItem_Model> _MatchItem = new Property<ListItem_Model> { LocatorFunc = _MatchItemLocator };
        static Func<BindableBase, ValueContainer<ListItem_Model>> _MatchItemLocator = RegisterContainerLocator<ListItem_Model>("MatchItem", model => model.Initialize("MatchItem", ref model._MatchItem, ref _MatchItemLocator, _MatchItemDefaultValueFactory));
        static Func<ListItem_Model> _MatchItemDefaultValueFactory = () => { return default(ListItem_Model); };
        #endregion

        public ObservableCollection<ListItem_Model> SearchResults
        {
            get { return _SearchResultsLocator(this).Value; }
            set { _SearchResultsLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<ListItem_Model> SearchResults Setup
        protected Property<ObservableCollection<ListItem_Model>> _SearchResults = new Property<ObservableCollection<ListItem_Model>> { LocatorFunc = _SearchResultsLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<ListItem_Model>>> _SearchResultsLocator = RegisterContainerLocator<ObservableCollection<ListItem_Model>>("SearchResults", model => model.Initialize("SearchResults", ref model._SearchResults, ref _SearchResultsLocator, _SearchResultsDefaultValueFactory));
        static Func<ObservableCollection<ListItem_Model>> _SearchResultsDefaultValueFactory = () => { return default(ObservableCollection<ListItem_Model>); };
        #endregion

        public ListItem_Model SelectedSearchResult
        {
            get { return _SelectedSearchResultLocator(this).Value; }
            set { _SelectedSearchResultLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ListItem_Model SelectedSearchResult Setup
        protected Property<ListItem_Model> _SelectedSearchResult = new Property<ListItem_Model> { LocatorFunc = _SelectedSearchResultLocator };
        static Func<BindableBase, ValueContainer<ListItem_Model>> _SelectedSearchResultLocator = RegisterContainerLocator<ListItem_Model>("SelectedSearchResult", model => model.Initialize("SelectedSearchResult", ref model._SelectedSearchResult, ref _SelectedSearchResultLocator, _SelectedSearchResultDefaultValueFactory));
        static Func<ListItem_Model> _SelectedSearchResultDefaultValueFactory = () => { return default(ListItem_Model); };
        #endregion

        public PagingInfo_Model PagingInfo
        {
            get { return _PagingInfoLocator(this).Value; }
            set { _PagingInfoLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property PagingInfo_Model PagingInfo Setup
        protected Property<PagingInfo_Model> _PagingInfo = new Property<PagingInfo_Model> { LocatorFunc = _PagingInfoLocator };
        static Func<BindableBase, ValueContainer<PagingInfo_Model>> _PagingInfoLocator = RegisterContainerLocator<PagingInfo_Model>("PagingInfo", model => model.Initialize("PagingInfo", ref model._PagingInfo, ref _PagingInfoLocator, _PagingInfoDefaultValueFactory));
        static Func<PagingInfo_Model> _PagingInfoDefaultValueFactory = () => { return default(PagingInfo_Model); };
        #endregion

        public ObservableCollection<string> Suggestions
        {
            get { return _SuggestionsLocator(this).Value; }
            set { _SuggestionsLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<string> Suggestions Setup
        protected Property<ObservableCollection<string>> _Suggestions = new Property<ObservableCollection<string>> { LocatorFunc = _SuggestionsLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<string>>> _SuggestionsLocator = RegisterContainerLocator<ObservableCollection<string>>("Suggestions", model => model.Initialize("Suggestions", ref model._Suggestions, ref _SuggestionsLocator, _SuggestionsDefaultValueFactory));
        static Func<ObservableCollection<string>> _SuggestionsDefaultValueFactory = () => { return default(ObservableCollection<string>); };
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
                            vm.ClearMatchItemAndSearchResult();
                            using (var api = new ApiClient())
                            {
                                var result =
                                    await api.ZhAsoiafWiki.Plus.Search(vm.SearchText);
                                vm.ShowSearchResult(result);
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
                            vm.PagingInfo = vm.PagingInfo ?? new PagingInfo_Model();
                            using (var api = new ApiClient())
                            {
                                var result =
                                    await
                                    api.ZhAsoiafWiki.Plus.Search(
                                        new SearchCriteria
                                            {
                                                Query = vm.SearchText,
                                                Page = vm.PagingInfo.CurrentPage + 1,
                                                PageSize = vm.PagingInfo.PageSize
                                            });
                                vm.ShowSearchResult(result);
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
                            var navigateTo = vm.MatchItem ?? vm.SelectedSearchResult;
                            if (navigateTo == null)
                            {
                                return;
                            }
                            var newVm = ViewModelLocator<ArticleDetailPage_Model>.Instance.Resolve();
                            newVm.Title = navigateTo.Title;
                            await CastToCurrentType(model).StageManager.DefaultStage.Show(newVm);
                        })
                    .DisposeWith(model);

                var cmdmdl = cmd.CreateCommandModel(resource);
                return cmdmdl;
            };
        #endregion

        public SearchPage_Model()
        {
            SearchResults = new ObservableCollection<ListItem_Model>();
            Suggestions = new ObservableCollection<string>();
            MatchItemPanelVisibility = Visibility.Collapsed;
            SearchResultPanelVisibility = Visibility.Collapsed;
            if (IsInDesignMode)
            {
                MatchItem = new ListItem_Model
                                {
                                    Title = "提利昂·兰尼斯特",
                                    Content =
                                        "提利昂·兰尼斯特（Tyrion Lannister）是泰温公爵和乔安娜夫人的第三个也是最小的孩子。因为是个侏儒，他有时候被戏称为小恶魔和半人。他利用自己的智慧屡次化险为夷，帮助兰尼斯特家族赢得了五王之战，但命运的不公使得他成为了一个弑亲者和通缉犯，踏上了流亡之路。 提利昂是书中一个非常重要的POV人物。在电视剧中，由皮特·丁拉基扮演。",
                                    ImageSource =
                                        "http://vignette3.wikia.nocookie.net/asoiaf/images/0/04/Tyrion_Lannister_personal_arms.png/revision/latest/window-crop/width/200/x-offset/0/y-offset/0/window-width/400/window-height/400?cb=20120205044455&path-prefix=zh"
                                };

                SearchResults.Add(
                    new ListItem_Model
                        {
                            Title = "提利昂·兰尼斯特",
                            SubTitle = "提利昂·兰尼斯特（Tyrion Lannister）是泰温公爵和乔安娜夫人的第三个也是最小的孩子。因为是个侏儒，他有时候被戏称为小恶魔和半人。他利用自己的智慧屡次化险为夷，帮助兰尼斯特家族赢得了五王之战，但命运的不公使得他成为了一个弑亲者和通缉犯，踏上了流亡之路。 提利昂是书中一个非常重要的POV人物。在电视剧中，由皮特·丁拉基扮演。",
                            ImageSource =
                                "http://vignette3.wikia.nocookie.net/asoiaf/images/0/04/Tyrion_Lannister_personal_arms.png/revision/latest/window-crop/width/200/x-offset/0/y-offset/0/window-width/400/window-height/400?cb=20120205044455&path-prefix=zh"
                        });
                SearchResults.Add(
                    new ListItem_Model
                    {
                        Title = "凯特琳·徒利",
                        ImageSource =
                            "http://vignette3.wikia.nocookie.net/asoiaf/images/6/67/Catelyn_Stark.jpg/revision/latest/window-crop/width/200/x-offset/0/y-offset/0/window-width/300/window-height/300?cb=20120206101506&path-prefix=zh"
                    });
                SearchResults.Add(new ListItem_Model { Title = "某某某", ImageSource = AppResources.PlaceholderImageSource });
                PagingInfo = new PagingInfo_Model { TotalCount = SearchResults.Count };
                SearchResultPanelVisibility = Visibility.Visible;
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

        /// <summary>
        /// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        /// </summary>
        /// <param name="view">View that firing Load event</param>
        /// <returns>Task awaiter</returns>
        protected override async Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            await base.OnBindedViewLoad(view);

            GetValueContainer(m => m.SearchText).GetNewValueObservable().Subscribe(
                async e =>
                {
                    var prefix = e.EventArgs;
                    if (string.IsNullOrEmpty(prefix) || prefix.Length < 4)
                    {
                        return;
                    }
                    using (var api = new ApiClient())
                    {
                        var result
                            = await api.MediaWiki.OpenSearch.GetSearchSuggestionAsync(prefix);
                        Suggestions.Clear();
                        Suggestions.AddRange(result);
                    }
                }).DisposeWith(this);
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

        private void ClearMatchItemAndSearchResult()
        {
            MatchItem = null;
            SearchResults.Clear();
            Suggestions.Clear();
            SelectedSearchResult = null;
            PagingInfo = null;
        }

        private void ShowSearchResult(SearchResult result)
        {
            if (result.MatchArticle == null)
            {
                PagingInfo = PagingInfo ?? new PagingInfo_Model();
                PagingInfo.TotalCount = result.TotalCount;
                PagingInfo.PageCount = result.GetPageCount();
                PagingInfo.CurrentPage = result.Page;
                PagingInfo.PageSize = result.PageSize;
                PagingInfo.LoadNextPageOffset = result.PageSize / 5;
                SearchResults.AddRange(
                    result.Articles.Select(
                        article =>
                        article.ToListItemModel(
                            img =>
                            WikiaImageFormatter.ToSquareImageThumbnailUri(
                                img,
                                article.ImageWidth,
                                article.ImageHeight,
                                ListItem_Model.ListModeImageSize))));
                MatchItemPanelVisibility = Visibility.Collapsed;
                SearchResultPanelVisibility = Visibility.Visible;
            }
            else
            {
                MatchItem =
                    result.MatchArticle.ToListItemModel(
                        img =>
                        WikiaImageFormatter.ToImageThumbnailUri(
                            img,
                            Math.Min(result.MatchArticle.ImageHeight, result.MatchArticle.ImageWidth) / 2));
                MatchItemPanelVisibility = Visibility.Visible;
                SearchResultPanelVisibility = Visibility.Collapsed;
            }
        }
    }

}

