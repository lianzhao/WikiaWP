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

using WikiaWP;
using WikiaWP.ViewModels;


namespace WikiaWP
{
    public partial class ArticleDetailPage : MVVMPage
    {



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

        private void WebBrowser_OnLoaded(object sender, RoutedEventArgs e)
        {
            NavigateToWikiPage("%E5%87%AF%E7%89%B9%E7%90%B3%C2%B7%E5%BE%92%E5%88%A9");
        }

        private void WebBrowser_OnNavigating(object sender, NavigatingEventArgs e)
        {
            const string NormalUriPrefix = "http://zh.asoiaf.wikia.com/wiki/";
            var uri = e.Uri.AbsoluteUri;
            if (uri.StartsWith(NormalUriPrefix, StringComparison.OrdinalIgnoreCase))
            {
                e.Cancel = true;
                NavigateToWikiPage(uri.Substring(NormalUriPrefix.Length));
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
            WebBrowser.Navigate(
                new Uri(
                    string.Format(
                        "http://zh.asoiaf.wikia.com/wikia.php?controller=GameGuides&method=renderFullPage&page={0}",
                        title)));
        }
    }
}




