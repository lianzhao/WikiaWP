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
using Microsoft.Phone.Shell;

using WikiaWP;
using WikiaWP.Models;
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

            vm.CommandLoadArticles.Execute(null);
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

        private void LongListSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = LayoutRoot.DataContext as CategoryListPage_Model;
            var control = sender as LongListSelector;
            if (vm == null || control == null || control.SelectedItem == null)
            {
                return;
            }

            var item = control.SelectedItem as ListItem_Model;
            if (item == null)
            {
                return;
            }

            vm.SelectedItem = item;
            control.SelectedItem = null;
            vm.CommandNavigateToSelected.Execute(null);
        }
    }
}




