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

using MediaWiki.Query.CategoryMembers;

using WikiaWP.Models;
using WikiaWP.Resources;

namespace WikiaWP.ViewModels
{

    //[DataContract]
    public class CategoryListPage_Model : ViewModelBase<CategoryListPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性

        public bool IsCuratedContent { get; set; }

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

        public ObservableCollection<ListItem_Model> Categories
        {
            get { return _CategoriesLocator(this).Value; }
            set { _CategoriesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<ListItem_Model> Categories Setup
        protected Property<ObservableCollection<ListItem_Model>> _Categories = new Property<ObservableCollection<ListItem_Model>> { LocatorFunc = _CategoriesLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<ListItem_Model>>> _CategoriesLocator = RegisterContainerLocator<ObservableCollection<ListItem_Model>>("Categories", model => model.Initialize("Categories", ref model._Categories, ref _CategoriesLocator, _CategoriesDefaultValueFactory));
        static Func<ObservableCollection<ListItem_Model>> _CategoriesDefaultValueFactory = () => { return default(ObservableCollection<ListItem_Model>); };
        #endregion

        public ObservableCollection<ListItem_Model> Articles
        {
            get { return _ArticlesLocator(this).Value; }
            set { _ArticlesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<ListItem_Model> Articles Setup
        protected Property<ObservableCollection<ListItem_Model>> _Articles = new Property<ObservableCollection<ListItem_Model>> { LocatorFunc = _ArticlesLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<ListItem_Model>>> _ArticlesLocator = RegisterContainerLocator<ObservableCollection<ListItem_Model>>("Articles", model => model.Initialize("Articles", ref model._Articles, ref _ArticlesLocator, _ArticlesDefaultValueFactory));
        static Func<ObservableCollection<ListItem_Model>> _ArticlesDefaultValueFactory = () => { return default(ObservableCollection<ListItem_Model>); };
        #endregion

        public ListItem_Model SelectedItem
        {
            get { return _SelectedItemLocator(this).Value; }
            set { _SelectedItemLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ListItem_Model SelectedItem Setup
        protected Property<ListItem_Model> _SelectedItem = new Property<ListItem_Model> { LocatorFunc = _SelectedItemLocator };
        static Func<BindableBase, ValueContainer<ListItem_Model>> _SelectedItemLocator = RegisterContainerLocator<ListItem_Model>("SelectedItem", model => model.Initialize("SelectedItem", ref model._SelectedItem, ref _SelectedItemLocator, _SelectedItemDefaultValueFactory));
        static Func<ListItem_Model> _SelectedItemDefaultValueFactory = () => { return default(ListItem_Model); };
        #endregion
        
        public CommandModel<ReactiveCommand, String> CommandLoadArticles
        {
            get { return _CommandLoadArticlesLocator(this).Value; }
            set { _CommandLoadArticlesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLoadArticles Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandLoadArticles = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLoadArticlesLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoadArticlesLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLoadArticles", model => model.Initialize("CommandLoadArticles", ref model._CommandLoadArticles, ref _CommandLoadArticlesLocator, _CommandLoadArticlesDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoadArticlesDefaultValueFactory =
            model =>
            {
                var resource = "LoadArticles";           // Command resource  
                var commandId = "LoadArticles";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            if (vm.IsCuratedContent)
                            {
                                return;
                            }
                            using (var api = new ApiClient())
                            {
                                var result =
                                    await
                                    api.MediaWiki.Query.CategoryMembers.GetCategoryMembersAsync(
                                        string.Format("Category:{0}", vm.Title),
                                        new[] { CatergoryMemberType.page },
                                        25);
                                var articles =
                                    await
                                    api.Wikia.Articles.GetArticlesAsync(
                                        result.query.categorymembers.Select(cm => cm.pageid));
                                var pages =
                                    articles.Select(
                                        art =>
                                        new ListItem_Model
                                            {
                                                Title = art.title,
                                                ImageSource =
                                                    art.thumbnail == null
                                                        ? AppResources.PlaceholderImageSource
                                                        : art.ThumbnailFixYOffset
                                            }).ToList();
                                vm.Articles = new ObservableCollection<ListItem_Model>(pages);
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

        public CommandModel<ReactiveCommand, String> CommandLoadCategories
        {
            get { return _CommandLoadCategoriesLocator(this).Value; }
            set { _CommandLoadCategoriesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLoadCategories Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandLoadCategories = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLoadCategoriesLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoadCategoriesLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLoadCategories", model => model.Initialize("CommandLoadCategories", ref model._CommandLoadCategories, ref _CommandLoadCategoriesLocator, _CommandLoadCategoriesDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoadCategoriesDefaultValueFactory =
            model =>
            {
                var resource = "LoadCategories";           // Command resource  
                var commandId = "LoadCategories";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            using (var api = new ApiClient())
                            {
                                if (vm.IsCuratedContent)
                                {
                                    var result = await api.Wikia.CuratedContent.GetCuratedContentSectionAsync(vm.Title);
                                    //var cats = result.items.Select(item => new ListItem_Model
                                    //                                           {
                                    //                                               Title = item.title
                                    //                                           })
                                    var articles =
                                        await api.Wikia.Articles.GetArticlesAsync(result.items.Select(item => item.id));
                                    var subcats =
                                        result.items.Join(
                                            articles,
                                            item => item.id,
                                            art => art.id,
                                            (item, art) =>
                                            new ListItem_Model
                                                {
                                                    Title = item.title,
                                                    ImageSource =
                                                        art.thumbnail == null
                                                            ? AppResources.PlaceholderImageSource
                                                            : art.ThumbnailFixYOffset
                                                }).ToList();
                                    vm.Categories = new ObservableCollection<ListItem_Model>(subcats);
                                }
                                else
                                {
                                    var result =
                                        await
                                        api.MediaWiki.Query.CategoryMembers.GetCategoryMembersAsync(
                                            string.Format("Category:{0}", vm.Title),
                                            new[] { CatergoryMemberType.subcat },
                                            25);
                                    var articles =
                                        await
                                        api.Wikia.Articles.GetArticlesAsync(
                                            result.query.categorymembers.Select(cm => cm.pageid));
                                    var subcats =
                                        articles.Select(
                                            art =>
                                            new ListItem_Model
                                            {
                                                Title = art.title,
                                                ImageSource =
                                                    art.thumbnail == null
                                                        ? AppResources.PlaceholderImageSource
                                                        : art.ThumbnailFixYOffset
                                            }).ToList();
                                    vm.Categories = new ObservableCollection<ListItem_Model>(subcats);
                                }
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

