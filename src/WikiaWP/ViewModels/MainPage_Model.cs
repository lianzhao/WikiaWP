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
            List2 = new ObservableCollection<ListItem_Model>();
            if (IsInDesignMode)
            {
                List2.Add(
                    new ListItem_Model
                        {
                            Title = "电视剧",
                            ImageSource =
                                "http://vignette3.wikia.nocookie.net/asoiaf/images/7/7a/Unknown_TV.jpg/revision/latest/window-crop/width/200/x-offset/281/y-offset/0/window-width/721/window-height/720?cb=20120507130022&path-prefix=zh"
                        });
                List2.Add(
                    new ListItem_Model
                    {
                        Title = "理论推测",
                        ImageSource =
                            "http://vignette1.wikia.nocookie.net/asoiaf/images/e/ed/Jon_baby.png/revision/latest/window-crop/width/200/x-offset/282/y-offset/0/window-width/733/window-height/732?cb=20140912061846&path-prefix=zh"
                    });
                List2.Add(
                    new ListItem_Model
                    {
                        Title = "历史",
                        ImageSource =
                            "http://vignette4.wikia.nocookie.net/asoiaf/images/7/78/BookOfBrothersDayne.jpg/revision/latest/window-crop/width/200/x-offset/425/y-offset/0/window-width/1073/window-height/1072?cb=20140912060347&path-prefix=zh"
                    });
                List2.Add(
                    new ListItem_Model
                    {
                        Title = "小说",
                        ImageSource =
                            "http://vignette2.wikia.nocookie.net/asoiaf/images/1/1f/GRRM_Books_Slider.jpg/revision/latest/window-crop/width/200/x-offset/156/y-offset/0/window-width/361/window-height/360?cb=20140912061039&path-prefix=zh"
                    });
                List2.Add(
                    new ListItem_Model
                    {
                        Title = "其他",
                        ImageSource =AppResources.PlaceholderImageSource
                    });
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

        
        public ObservableCollection<ListItem_Model> List2
        {
            get { return _List2Locator(this).Value; }
            set { _List2Locator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<ListItem_Model> List2 Setup
        protected Property<ObservableCollection<ListItem_Model>> _List2 = new Property<ObservableCollection<ListItem_Model>> { LocatorFunc = _List2Locator };
        static Func<BindableBase, ValueContainer<ObservableCollection<ListItem_Model>>> _List2Locator = RegisterContainerLocator<ObservableCollection<ListItem_Model>>("List2", model => model.Initialize("List2", ref model._List2, ref _List2Locator, _List2DefaultValueFactory));
        static Func<ObservableCollection<ListItem_Model>> _List2DefaultValueFactory = () => { return default(ObservableCollection<ListItem_Model>); };
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
                            var categories = new[] { "人物", "贵族家族", "事件", "地理" };
                            using (var api = new ApiClient())
                            {
                                var apiClient = api;
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
                                vm.List2.AddRange(list);
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
            var content = await api.WikiaApi.CuratedContent.GetCuratedContentAsync();
            var articles =
                await api.WikiaApi.Articles.GetArticlesAsync(content.tags.Select(tag => tag.id));
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
            var articles = await api.WikiaApi.Articles.GetTopArticlesAsync(category, count);
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

