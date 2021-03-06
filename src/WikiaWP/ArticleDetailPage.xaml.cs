﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using LianZhao;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

using MVVMSidekick.Views;

using WikiaWP.Models;
using WikiaWP.ViewModels;

namespace WikiaWP
{
    public partial class ArticleDetailPage : MVVMPage
    {
        private const string NormalUriPrefix = "http://zh.asoiaf.wikia.com/wiki/";

        private const string ApiUriPrefix =
            "http://zh.asoiaf.wikia.com/wikia.php?controller=GameGuides&method=renderFullPage&page=";

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
            var vm = LayoutRoot.DataContext as ArticleDetailPage_Model;
            if (vm == null)
            {
                return;
            }

            var uri = e.Uri.AbsoluteUri;
            if (uri.Contains("action=edit", StringComparison.OrdinalIgnoreCase))
            {
                e.Cancel = true;
            }
            if (uri.StartsWith(NormalUriPrefix, StringComparison.OrdinalIgnoreCase))
            {
                e.Cancel = true;
                var newTitle = WebUtility.UrlDecode(uri.Substring(NormalUriPrefix.Length));
                if (newTitle != vm.Title)
                {
                    if (newTitle.Contains(":"))
                    {
                        var splited = newTitle.Split(':');
                        var ns = splited[0];
                        if (ns.Equals("TV", StringComparison.OrdinalIgnoreCase))
                        {
                            // do nothing
                        }
                        else if (ns.Equals("File", StringComparison.OrdinalIgnoreCase))
                        {
                            vm.CommandNavigateToFilePage.Execute(splited[1]);
                            return;
                        }
                        else if (ns.Equals("Category", StringComparison.OrdinalIgnoreCase))
                        {
                            // todo
                            var result = MessageBox.Show("想要前往该分类吗？", "您正要查看一个分类页面", MessageBoxButton.OKCancel);
                            if (result == MessageBoxResult.Cancel)
                            {
                                return;
                            }
                            else
                            {
                                var category = splited[1];
                                vm.CommandNavigateToCategoryPage.Execute(category);
                                return;
                            }
                        }
                        else
                        {
                            // invalid ns
                            return;
                        }
                    }
                    WebBrowser.Opacity = 0;
                    ProgressBar.Visibility = Visibility.Visible;
                    ProgressBar.IsIndeterminate = true;
                    Histories.Push(vm.Title);
                    NavigateToWikiPage(newTitle);
                    vm.ClearData();
                    vm.Title = newTitle;
                }
            }
            else if (uri.StartsWith(ApiUriPrefix, StringComparison.OrdinalIgnoreCase))
            {
                // do nothing, let it go
            }
            else
            {
                e.Cancel = true;
                var task = new WebBrowserTask();
                task.Uri = new Uri(uri, UriKind.Absolute);
                task.Show();
            }
        }

        private void WebBrowser_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            WebBrowser.Opacity = 1;
            ProgressBar.Visibility = Visibility.Collapsed;
            ProgressBar.IsIndeterminate = false;
        }

        private void NavigateToWikiPage(string title)
        {
            //MessageBox.Show(title);// todo
            //WebBrowser.IsScriptEnabled = true;

            WebBrowser.Navigate(
                new Uri(
                    string.Format(
                        "http://zh.asoiaf.wikia.com/wikia.php?controller=GameGuides&method=renderFullPage&page={0}",
                        title)));

            //var uri = "http://zh.asoiaf.wikia.com/wiki/%E5%87%AF%E7%89%B9%E7%90%B3%C2%B7%E5%BE%92%E5%88%A9";

            //var task = new WebBrowserTask();
            //task.Uri = new Uri(uri, UriKind.Absolute);
            //task.Show();

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




