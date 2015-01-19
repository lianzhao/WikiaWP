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

using Microsoft.Phone.Shell;

using WikiaWP;
using WikiaWP.ViewModels;


namespace WikiaWP
{
    public partial class CategoryListPage : MVVMPage
    {



        public CategoryListPage()
            : base(null)
        {
            this.InitializeComponent();
        }
        public CategoryListPage(CategoryListPage_Model model)
            : base(model)
        {
            this.InitializeComponent();
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

        private void ArticlesPivotItem_OnLoaded(object sender, RoutedEventArgs e)
        {
            var vm = LayoutRoot.DataContext as CategoryListPage_Model;
            if (vm == null)
            {
                return;
            }

            vm.CommandLoadPages.Execute(null);
        }

        private void CategoriesPivotItem_OnLoaded(object sender, RoutedEventArgs e)
        {
            var vm = LayoutRoot.DataContext as CategoryListPage_Model;
            if (vm == null)
            {
                return;
            }

            vm.CommandLoadCategories.Execute(null);
        }
    }
}




