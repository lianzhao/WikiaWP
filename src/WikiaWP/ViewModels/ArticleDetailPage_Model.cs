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
using System.Globalization;
using System.Runtime.Serialization;

using LianZhao.Patterns.Composite;

using WikiaWP.Models;
using WikiaWP.Resources;

namespace WikiaWP.ViewModels
{

    //[DataContract]
    public class ArticleDetailPage_Model : ViewModelBase<ArticleDetailPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性

        public ArticleDetailPage_Model()
        {
            if (IsInDesignMode)
            {
                Title = "凯特琳·徒利";
                Comments = new ObservableCollection<ArticleComment_Model>();
                Comments.Add(new ArticleComment_Model
                                 {
                                     Content = "凯特和瑟曦，一个是满不懂、一个是假行家。色太后基本什么权谋都玩不了，纯不会玩。凯特琳其实就是不懂装懂，和佛雷家谈判之前就应该想到人家会要联姻，其实当时佛雷家也就是想和比他们高贵的大贵族联姻就行，要个王后最好，其实当时北军手上还有个王子，那就是席恩，铁群岛正牌而且唯一的继承人，按计划还能打下凯岩城，徒利家凯特弟弟也很高贵。所以罗柏这个北境之王好比是大王，席恩铁群岛继承人和三河公爵艾德穆就好比是两个小王，珊莎、艾丽娅、瑞肯就好比小2，这些都可以用来联姻，抢在佛雷开口要罗柏之前，就以归还席恩为条件，让罗柏娶阿莎，并以阿莎为人质。再让席恩和艾德穆娶佛雷家，来补偿佛雷。席恩娶佛雷家的萝卜丝，我想他也没意见，在让他和艾德穆带着三河和佛雷家的兵力，汇合铁群岛的海军直取凯岩城，阿莎嫁给了罗柏话，并把西境许给铁岛，铁岛肯定不会背叛狼家。而罗柏按计划打败詹姆、再拖住泰温，而且要是阿莎在罗柏身边，这个姐姐绝对能搞定少狼主，不敢乱搞。再把珊莎送给提利尔家，狮子早就完蛋了凯特和瑟曦，一个是满不懂、一个是假行家。色太后基本什么权谋都玩不了，纯不会玩。凯特琳其实就是不懂装懂，和佛雷家谈判之前就应该想到人家会要联姻，其实当时佛雷家也就是想和比他们高贵的大贵族联姻就行，要个王后最好，其实当时北军手上还有个王子，那就是席恩，铁群岛正牌而且唯一的继承人，按计划还能打下凯岩城，徒利家凯特弟弟也很高贵。所以罗柏这个北境之王好比是大王，席恩铁群岛继承人和三河公爵艾德穆就好比是两个小王，珊莎、艾丽娅、瑞肯就好比小2，这些都可以用来联姻，抢在佛雷开口要罗柏之前，就以归还席恩为条件，让罗柏娶阿莎，并以阿莎为人质。再让席恩和艾德穆娶佛雷家，来补偿佛雷。席恩娶佛雷家的萝卜丝，我想他也没意见，在让他和艾德穆带着三河和佛雷家的兵力，汇合铁群岛的海军直取凯岩城，阿莎嫁给了罗柏话，并把西境许给铁岛，铁岛肯定不会背叛狼家。而罗柏按计划打败詹姆、再拖住泰温，而且要是阿莎在罗柏身边，这个姐姐绝对能搞定少狼主，不敢乱搞。再把珊莎送给提利尔家，狮子早就完蛋了",
                                     CreateDateTimeUtc = DateTime.UtcNow.AddDays(-10),
                                     CreateUser = "一个匿名参与者"
                                 });
                Comments.Add(new ArticleComment_Model
                {
                    Content = "最希望这个有点脑残的女人最后能把小指头杀掉",
                    CreateDateTimeUtc = DateTime.UtcNow.AddDays(-1),
                    CreateUser = "一个匿名参与者"
                });
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

        public ObservableCollection<ArticleComment_Model> Comments
        {
            get { return _CommentsLocator(this).Value; }
            set { _CommentsLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<ArticleComment_Model> Comments Setup
        protected Property<ObservableCollection<ArticleComment_Model>> _Comments = new Property<ObservableCollection<ArticleComment_Model>> { LocatorFunc = _CommentsLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<ArticleComment_Model>>> _CommentsLocator = RegisterContainerLocator<ObservableCollection<ArticleComment_Model>>("Comments", model => model.Initialize("Comments", ref model._Comments, ref _CommentsLocator, _CommentsDefaultValueFactory));
        static Func<ObservableCollection<ArticleComment_Model>> _CommentsDefaultValueFactory = () => { return default(ObservableCollection<ArticleComment_Model>); };
        #endregion

        public ObservableCollection<ListItem_Model> RelatedPages
        {
            get { return _RelatedPagesLocator(this).Value; }
            set { _RelatedPagesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<ListItem_Model> RelatedPages Setup
        protected Property<ObservableCollection<ListItem_Model>> _RelatedPages = new Property<ObservableCollection<ListItem_Model>> { LocatorFunc = _RelatedPagesLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<ListItem_Model>>> _RelatedPagesLocator = RegisterContainerLocator<ObservableCollection<ListItem_Model>>("RelatedPages", model => model.Initialize("RelatedPages", ref model._RelatedPages, ref _RelatedPagesLocator, _RelatedPagesDefaultValueFactory));
        static Func<ObservableCollection<ListItem_Model>> _RelatedPagesDefaultValueFactory = () => { return default(ObservableCollection<ListItem_Model>); };
        #endregion

        public ListItem_Model SelectedRelatedPage
        {
            get { return _SelectedRelatedPageLocator(this).Value; }
            set { _SelectedRelatedPageLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ListItem_Model SelectedRelatedPage Setup
        protected Property<ListItem_Model> _SelectedRelatedPage = new Property<ListItem_Model> { LocatorFunc = _SelectedRelatedPageLocator };
        static Func<BindableBase, ValueContainer<ListItem_Model>> _SelectedRelatedPageLocator = RegisterContainerLocator<ListItem_Model>("SelectedRelatedPage", model => model.Initialize("SelectedRelatedPage", ref model._SelectedRelatedPage, ref _SelectedRelatedPageLocator, _SelectedRelatedPageDefaultValueFactory));
        static Func<ListItem_Model> _SelectedRelatedPageDefaultValueFactory = () => { return default(ListItem_Model); };
        #endregion

        public string CommentsHeaderText
        {
            get { return _CommentsHeaderTextLocator(this).Value; }
            set { _CommentsHeaderTextLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string CommentsHeaderText Setup
        protected Property<string> _CommentsHeaderText = new Property<string> { LocatorFunc = _CommentsHeaderTextLocator };
        static Func<BindableBase, ValueContainer<string>> _CommentsHeaderTextLocator = RegisterContainerLocator<string>("CommentsHeaderText", model => model.Initialize("CommentsHeaderText", ref model._CommentsHeaderText, ref _CommentsHeaderTextLocator, _CommentsHeaderTextDefaultValueFactory));
        static Func<string> _CommentsHeaderTextDefaultValueFactory = () => { return default(string); };
        #endregion

        public CommandModel<ReactiveCommand, String> CommandLoadComments
        {
            get { return _CommandLoadCommentsLocator(this).Value; }
            set { _CommandLoadCommentsLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLoadComments Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandLoadComments = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLoadCommentsLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoadCommentsLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLoadComments", model => model.Initialize("CommandLoadComments", ref model._CommandLoadComments, ref _CommandLoadCommentsLocator, _CommandLoadCommentsDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoadCommentsDefaultValueFactory =
            model =>
            {
                var resource = "LoadComments";           // Command resource  
                var commandId = "LoadComments";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                            {
                                using (var api = new ApiClient())
                                {
                                    var comments =
                                        await api.WikiaApi.Mercury.GetArticleCommentsAsync(vm.Title);
                                    var commentModels =
                                        comments.payload.comments.OrderByDescending(c => c.CreatedUtc)
                                            .Select(c => c.ToArticleComment_Model(comments));
                                    vm.CommentsHeaderText = string.Format(
                                        "共{0}条评论",
                                        comments.payload.GetAllDescendants().Count().ToString(CultureInfo.InvariantCulture));
                                    vm.Comments = new ObservableCollection<ArticleComment_Model>(commentModels);
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

        public CommandModel<ReactiveCommand, String> CommandLoadRelatedPages
        {
            get { return _CommandLoadRelatedPagesLocator(this).Value; }
            set { _CommandLoadRelatedPagesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLoadRelatedPages Setup
        protected Property<CommandModel<ReactiveCommand, String>> _CommandLoadRelatedPages = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLoadRelatedPagesLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoadRelatedPagesLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLoadRelatedPages", model => model.Initialize("CommandLoadRelatedPages", ref model._CommandLoadRelatedPages, ref _CommandLoadRelatedPagesLocator, _CommandLoadRelatedPagesDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoadRelatedPagesDefaultValueFactory =
            model =>
            {
                var resource = "LoadRelatedPages";           // Command resource  
                var commandId = "LoadRelatedPages";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd
                    .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            using (var api = new ApiClient())
                            {
                                var id = 0;
                                if (vm.TryGetIdFromTitle(out id))
                                {
                                    var pages = await api.WikiaApi.RelatedPages.GetRelatedPagesAsync(id);
                                    if (pages == null)
                                    {
                                        return;
                                    }
                                    var list =
                                        pages.Select(
                                            p =>
                                            new ListItem_Model
                                                {
                                                    Title = p.title,
                                                    Content = p.text,
                                                    ImageSource =
                                                        p.imgUrl ?? AppResources.PlaceholderImageSource
                                                });
                                    vm.RelatedPages = new ObservableCollection<ListItem_Model>(list);
                                }
                                else
                                {
                                    var article = await api.WikiaApi.Mercury.GetArticleAsync(vm.Title);
                                    if (article == null)
                                    {
                                        return;
                                    }
                                    var list =
                                        article.data.relatedPages.Select(
                                            p =>
                                            new ListItem_Model
                                                {
                                                    Title = p.title,
                                                    Content = p.text,
                                                    ImageSource =
                                                        p.imgUrl == null
                                                            ? AppResources.PlaceholderImageSource
                                                            : p.FixImgUrl()
                                                });
                                    vm.RelatedPages = new ObservableCollection<ListItem_Model>(list);
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

        public void ClearData()
        {
            Title = null;
            Comments = null;
            RelatedPages = null;
            SelectedRelatedPage = null;
            CommentsHeaderText = null;
        }

        private bool TryGetIdFromTitle(out int id)
        {
            // todo get from cached title/id dict
            id = 0;
            return false;
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

