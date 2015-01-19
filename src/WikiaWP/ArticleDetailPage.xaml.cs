using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using WikiaWP;
using WikiaWP.Models;
using WikiaWP.ViewModels;


namespace WikiaWP
{
    public partial class ArticleDetailPage : MVVMPage
    {
        private readonly Stack<string> Histories = new Stack<string>();

        public ArticleDetailPage()
            : base(null)
        {
            this.InitializeComponent();
        }
        public ArticleDetailPage(ArticleDetailPage_Model model)
            : base(model)
        {
            this.InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (WebBrowser.CanGoBack)
            {
                e.Cancel = true;
                WebBrowser.GoBack();
                var title = Histories.Pop();
                var vm = LayoutRoot.DataContext as ArticleDetailPage_Model;
                vm.ClearData();
                vm.Title = title;
                Pivot.SelectedItem = OverviewPivotItem;
                return;
            }
            base.OnBackKeyPress(e);
        }

        private void WebBrowser_OnLoaded(object sender, RoutedEventArgs e)
        {
            var vm = LayoutRoot.DataContext as ArticleDetailPage_Model;
            if (vm == null)
            {
                return;
            }

            NavigateToWikiPage(vm.Title);
        }

        private void WebBrowser_OnNavigating(object sender, NavigatingEventArgs e)
        {
            const string NormalUriPrefix = "http://zh.asoiaf.wikia.com/wiki/";
            var vm = LayoutRoot.DataContext as ArticleDetailPage_Model;
            if (vm == null)
            {
                return;
            }

            var uri = e.Uri.AbsoluteUri;
            if (uri.StartsWith(NormalUriPrefix, StringComparison.OrdinalIgnoreCase))
            {
                e.Cancel = true;
                Histories.Push(vm.Title);
                var newTitle = uri.Substring(NormalUriPrefix.Length);
                NavigateToWikiPage(newTitle);
                vm.ClearData();
                vm.Title = WebUtility.UrlDecode(newTitle);
            }
            WebBrowser.Opacity = 0;
            ProgressBar.Visibility = Visibility.Visible;
            ProgressBar.IsIndeterminate = true;
        }

        private void WebBrowser_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            WebBrowser.Opacity = 1;
            ProgressBar.Visibility = Visibility.Collapsed;
            ProgressBar.IsIndeterminate = false;
        }

        private void NavigateToWikiPage(string title)
        {
            //WebBrowser.IsScriptEnabled = true;

            WebBrowser.Navigate(
                new Uri(
                    string.Format(
                        "http://zh.asoiaf.wikia.com/wikia.php?controller=GameGuides&method=renderFullPage&page={0}",
                        title)));

            //WebBrowser.Navigate(
            //    new Uri(
            //        string.Format(
            //            "http://zh.asoiaf.wikia.com/wikia.php?controller=GameGuides&method=renderFullPage&page={0}",
            //            title)),
            //    null,
            //    "User-Agent: Mozilla/5.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) AppleWebKit/600.1.3 (KHTML, like Gecko) Version/8.0 Mobile/12A4345d Safari/600.1.4");

        }

        private void Pivot_OnLoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            var vm = LayoutRoot.DataContext as ArticleDetailPage_Model;
            if (vm == null)
            {
                return;
            }
            if (e.Item == CommentsPivotItem)
            {
                vm.CommandLoadComments.Execute(null);
            }
            else if (e.Item == RelatedPagesPivotItem)
            {
                vm.CommandLoadRelatedPages.Execute(null);
            }
        }

        private void ApplicationBarMenuItem_OnClick(object sender, EventArgs e)
        {
            // todo
            while (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            //var vm = LayoutRoot.DataContext as ArticleDetailPage_Model;
            //if (vm == null)
            //{
            //    return;
            //}
            //vm.CommandNavigateToMainPage.Execute(null);
        }

        private void ApplicationBar_OnStateChanged(object sender, ApplicationBarStateChangedEventArgs e)
        {
            var appbar = sender as ApplicationBar;
            if (appbar == null)
            {
                return;
            }
            appbar.Opacity = e.IsMenuVisible ? 1 : 0.25;
        }

        private void RelatedPagesLongListSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var control = sender as LongListSelector;
            var vm = LayoutRoot.DataContext as ArticleDetailPage_Model;
            if (vm == null || control == null || control.SelectedItem == null)
            {
                return;
            }
            var item = control.SelectedItem as ListItem_Model;
            if (item == null)
            {
                return;
            }
            Pivot.SelectedIndex = 0;
            Histories.Push(vm.Title);
            NavigateToWikiPage(item.Title);
            vm.ClearData();
            vm.Title = item.Title;
        }

        private void GestureListener_Flick(object sender, FlickGestureEventArgs e)
        {
            if (e.Direction == System.Windows.Controls.Orientation.Horizontal)
            {
                Pivot.SelectedIndex = e.HorizontalVelocity > 0 ? 2 : 1;
            }

        }
    }
}




