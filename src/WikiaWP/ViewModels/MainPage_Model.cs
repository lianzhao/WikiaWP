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

using Windows.Media.MediaProperties;

using LianZhao.Collections.Generic;
using LianZhao.Linq;

using Wikia.Asoiaf.Zh.Pages;

using WikiaWP.Models;
using WikiaWP.Resources;

namespace WikiaWP.ViewModels
{
    //[DataContract]
    public class MainPage_Model : ViewModelBase<MainPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

        public MainPage_Model()
        {
            List1 = new ObservableCollection<IGrouping<string, ListItem_Model>>();
            List2 = new ObservableCollection<IGrouping<string, ListItem_Model>>();
            List3 = new ObservableCollection<IGrouping<string, ListItem_Model>>();
            if (IsInDesignMode)
            {
            }
        }

        //propvm tab tab string tab Title

        public ObservableCollection<IGrouping<string, ListItem_Model>> List1
        {
            get { return _List1Locator(this).Value; }
            set { _List1Locator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<IGrouping<string, ListItem_Model>> List1 Setup
        protected Property<ObservableCollection<IGrouping<string, ListItem_Model>>> _List1 = new Property<ObservableCollection<IGrouping<string, ListItem_Model>>> { LocatorFunc = _List1Locator };
        static Func<BindableBase, ValueContainer<ObservableCollection<IGrouping<string, ListItem_Model>>>> _List1Locator = RegisterContainerLocator<ObservableCollection<IGrouping<string, ListItem_Model>>>("List1", model => model.Initialize("List1", ref model._List1, ref _List1Locator, _List1DefaultValueFactory));
        static Func<ObservableCollection<IGrouping<string, ListItem_Model>>> _List1DefaultValueFactory = () => { return default(ObservableCollection<IGrouping<string, ListItem_Model>>); };
        #endregion

        public ObservableCollection<IGrouping<string, ListItem_Model>> List2
        {
            get { return _List2Locator(this).Value; }
            set { _List2Locator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<IGrouping<string, ListItem_Model>> List2 Setup
        protected Property<ObservableCollection<IGrouping<string, ListItem_Model>>> _List2 = new Property<ObservableCollection<IGrouping<string, ListItem_Model>>> { LocatorFunc = _List2Locator };
        static Func<BindableBase, ValueContainer<ObservableCollection<IGrouping<string, ListItem_Model>>>> _List2Locator = RegisterContainerLocator<ObservableCollection<IGrouping<string, ListItem_Model>>>("List2", model => model.Initialize("List2", ref model._List2, ref _List2Locator, _List2DefaultValueFactory));
        static Func<ObservableCollection<IGrouping<string, ListItem_Model>>> _List2DefaultValueFactory = () => { return default(ObservableCollection<IGrouping<string, ListItem_Model>>); };
        #endregion

        public ObservableCollection<IGrouping<string, ListItem_Model>> List3
        {
            get { return _List3Locator(this).Value; }
            set { _List3Locator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<IGrouping<string, ListItem_Model>> List3 Setup
        protected Property<ObservableCollection<IGrouping<string, ListItem_Model>>> _List3 = new Property<ObservableCollection<IGrouping<string, ListItem_Model>>> { LocatorFunc = _List3Locator };
        static Func<BindableBase, ValueContainer<ObservableCollection<IGrouping<string, ListItem_Model>>>> _List3Locator = RegisterContainerLocator<ObservableCollection<IGrouping<string, ListItem_Model>>>("List3", model => model.Initialize("List3", ref model._List3, ref _List3Locator, _List3DefaultValueFactory));
        static Func<ObservableCollection<IGrouping<string, ListItem_Model>>> _List3DefaultValueFactory = () => { return default(ObservableCollection<IGrouping<string, ListItem_Model>>); };
        #endregion
        
        public ListItem_Model SelectedItem
        {
            get { return _SelectedItemLocator(this).Value; }
            set { _SelectedItemLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ListItem_Model SelectedItem Setup        
        protected Property<ListItem_Model> _SelectedItem = new Property<ListItem_Model>{ LocatorFunc = _SelectedItemLocator};
        static Func<BindableBase,ValueContainer<ListItem_Model>> _SelectedItemLocator= RegisterContainerLocator<ListItem_Model>("SelectedItem", model =>model.Initialize("SelectedItem",ref model._SelectedItem, ref _SelectedItemLocator,_SelectedItemDefaultValueFactory));         
        static Func<ListItem_Model> _SelectedItemDefaultValueFactory = ()=>{ return default(ListItem_Model); };
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

            await ExecuteTask(
                async () =>
                {
                    // todo load cache
                });
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


        public CommandModel<ReactiveCommand, String> CommandNavigateToSearch
        {
            get { return _CommandNavigateToSearchLocator(this).Value; }
            set { _CommandNavigateToSearchLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandNavigateToSearch Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandNavigateToSearch = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandNavigateToSearchLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandNavigateToSearchLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandNavigateToSearch", model => model.Initialize("CommandNavigateToSearch", ref model._CommandNavigateToSearch, ref _CommandNavigateToSearchLocator, _CommandNavigateToSearchDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandNavigateToSearchDefaultValueFactory =
            model =>
            {
                var resource = "NavigateToSearch";           // Command resource  
                var commandId = "NavigateToSearch";
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .Subscribe(async _ =>
                    {
                        var vms = await
                            CastToCurrentType(model)
                            .StageManager
                            .DefaultStage
                            .ShowAndGetViewModel<SearchPage_Model>();
                        await vms.Closing;
                    })
                    .DisposeWith(model);

                var cmdmdl = cmd.CreateCommandModel(resource);
                return cmdmdl;
            };
        #endregion

        public CommandModel<ReactiveCommand, String> CommandNavigateToSelected
        {
            get { return _CommandNavigateToSelectedLocator(this).Value; }
            set { _CommandNavigateToSelectedLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandNavigateToSelected Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandNavigateToSelected = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandNavigateToSelectedLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandNavigateToSelectedLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandNavigateToSelected", model => model.Initialize("CommandNavigateToSelected", ref model._CommandNavigateToSelected, ref _CommandNavigateToSelectedLocator, _CommandNavigateToSelectedDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandNavigateToSelectedDefaultValueFactory =
            model =>
            {
                var resource = "NavigateToSelected";           // Command resource  
                var commandId = "NavigateToSelected";
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .Subscribe(async _ =>
                        {
                            var vm = CastToCurrentType(model);
                        var vms = await vm
                            .StageManager
                            .DefaultStage
                            .ShowAndGetViewModel<ArticleDetailPage_Model>();
                            vms.ViewModel.Title = vm.SelectedItem.Title;
                        await vms.Closing;
                    })
                    .DisposeWith(model);

                var cmdmdl = cmd.CreateCommandModel(resource);
                return cmdmdl;
            };
        #endregion


        public CommandModel<ReactiveCommand, String> CommandNavigateToAbout
        {
            get { return _CommandNavigateToAboutLocator(this).Value; }
            set { _CommandNavigateToAboutLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandNavigateToAbout Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandNavigateToAbout = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandNavigateToAboutLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandNavigateToAboutLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandNavigateToAbout", model => model.Initialize("CommandNavigateToAbout", ref model._CommandNavigateToAbout, ref _CommandNavigateToAboutLocator, _CommandNavigateToAboutDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandNavigateToAboutDefaultValueFactory =
            model =>
            {
                var resource = "NavigateToAbout";           // Command resource  
                var commandId = "NavigateToAbout";
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .Subscribe(async _ =>
                    {
                        var vms = await
                            CastToCurrentType(model)
                            .StageManager
                            .DefaultStage
                            .ShowAndGetViewModel<AboutPage_Model>();
                        vms.ViewModel.Init();
                        await vms.Closing;
                    })
                    .DisposeWith(model);

                var cmdmdl = cmd.CreateCommandModel(resource);
                return cmdmdl;
            };
        #endregion


        public CommandModel<ReactiveCommand, String> CommandLoadList1
        {
            get { return _CommandLoadList1Locator(this).Value; }
            set { _CommandLoadList1Locator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLoadList1 Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandLoadList1 = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLoadList1Locator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoadList1Locator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLoadList1", model => model.Initialize("CommandLoadList1", ref model._CommandLoadList1, ref _CommandLoadList1Locator, _CommandLoadList1DefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoadList1DefaultValueFactory =
            model =>
            {
                var resource = "LoadList1";           // Command resource  
                var commandId = "LoadList1";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            vm.List1.Clear();
                            using (var api = new ApiClient())
                            {
                                var apiClient = api;
                                var categories = await apiClient.ZhAsoiafWiki.Categories.GetTopCategoriesAsync();
                                var tasks = categories.Select(c => vm.GetTopArticlesAsync(apiClient, c, count: 7));
                                var list = await Task.WhenAll(tasks);
                                // LongListSelector grouping feature not work with IEnumerable...
                                // http://stackoverflow.com/questions/13479727/grouped-longlistselector-headers-appear-items-dont
                                vm.List1.AddRange(
                                    list.SelectMany(m => m)
                                        .GroupBy(m => m.Group)
                                        .Select(group => new GroupingList<string, ListItem_Model>(group)));
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

        public CommandModel<ReactiveCommand, String> CommandLoadList2
        {
            get { return _CommandLoadList2Locator(this).Value; }
            set { _CommandLoadList2Locator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLoadList2 Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandLoadList2 = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLoadList2Locator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoadList2Locator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLoadList2", model => model.Initialize("CommandLoadList2", ref model._CommandLoadList2, ref _CommandLoadList2Locator, _CommandLoadList2DefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoadList2DefaultValueFactory =
            model =>
            {
                var resource = "LoadList2";           // Command resource  
                var commandId = "LoadList2";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            vm.List2.Clear();
                            using (var api = new ApiClient())
                            {
                                var list = await vm.GetCuratedContentAsync(api);
                                vm.List2.AddRange(
                                    list.GroupBy(m => m.Group)
                                        .Select(group => new GroupingList<string, ListItem_Model>(group)));
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

        public CommandModel<ReactiveCommand, String> CommandLoadList3
        {
            get { return _CommandLoadList3Locator(this).Value; }
            set { _CommandLoadList3Locator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLoadList3 Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandLoadList3 = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLoadList3Locator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoadList3Locator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLoadList3", model => model.Initialize("CommandLoadList3", ref model._CommandLoadList3, ref _CommandLoadList3Locator, _CommandLoadList3DefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoadList3DefaultValueFactory =
            model =>
            {
                var resource = "LoadList3";           // Command resource  
                var commandId = "LoadList3";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            vm.List3.Clear();
                            using (var api = new ApiClient())
                            {
                                var t1 = vm.GetList3Group(api.ZhAsoiafWiki.Pages.GetWikiNews(), "维基动态");
                                var t2 = vm.GetList3Group(api.ZhAsoiafWiki.Pages.GetRecommendationsAsync(), "推荐阅读");
                                var groups = await Task.WhenAll(new[] { t1, t2 });
                                // LongListSelector grouping feature not work with IEnumerable...
                                // http://stackoverflow.com/questions/13479727/grouped-longlistselector-headers-appear-items-dont
                                vm.List3.AddRange(
                                    groups.SelectMany(item => item)
                                        .GroupBy(m => m.Group)
                                        .Select(group => new GroupingList<string, ListItem_Model>(group)));
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


        private async Task<IEnumerable<ListItem_Model>> GetCuratedContentAsync(ApiClient api)
        {
            var content = await api.Wikia.CuratedContent.GetCuratedContentAsync();
            var articles =
                await api.Wikia.Articles.GetArticlesAsync(content.tags.Select(tag => tag.id));
            return content.tags.Join(
                articles,
                tag => tag.id,
                article => article.id,
                (tag, article) =>
                new ListItem_Model
                    {
                        Title = tag.title,
                        ImageSource = article.thumbnail ?? AppResources.PlaceholderImageSource,
                        Group = "精选"
                    });
        }

        private async Task<IEnumerable<ListItem_Model>> GetTopArticlesAsync(ApiClient api, string category, int count)
        {
            var articles = await api.Wikia.Articles.GetTopArticlesAsync(category, count);
            var group = string.Format("最受欢迎{0}", category);
            return
                articles.Select(
                    art =>
                    new ListItem_Model
                        {
                            Title = art.title,
                            Content = art.@abstract,
                            ImageSource = art.ThumbnailFixYOffset ?? AppResources.PlaceholderImageSource,
                            Group = group
                        }).Concat(CreateLoadMoreListItem(group));
        }

        private async Task<IEnumerable<ListItem_Model>> GetList3Group(Task<IEnumerable<PageItem>> task, string group)
        {
            var items = await task;
            return
                items.Select(
                    item =>
                    new ListItem_Model
                        {
                            Title = item.title,
                            SubTitle = item.@abstract,
                            ImageSource = item.thumbnail,
                            Content = item.title,
                            Group = group
                        });
        }

        private static ListItem_Model CreateLoadMoreListItem(string group)
        {
            return new ListItem_Model
                       {
                           Title = "更多...",
                           ImageSource = AppResources.PlaceholderImageSource,
                           Group = group
                       };
        }
    }

}

