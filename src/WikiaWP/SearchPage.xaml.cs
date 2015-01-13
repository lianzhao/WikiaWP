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
using WikiaWP.Models;
using WikiaWP.ViewModels;


namespace WikiaWP
{
    public partial class SearchPage : MVVMPage
    {



        public SearchPage()
            : base(null)
        {
            this.InitializeComponent();
        }

        public SearchPage(SearchPage_Model model)
            : base(model)
        {
            this.InitializeComponent();
        }

        private void SearchTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Focus();// hide keyboard
                var vm = LayoutRoot.DataContext as SearchPage_Model;
                var binding = SearchTextBox.GetBindingExpression(TextBox.TextProperty);
                binding.UpdateSource();
                vm.CommandSearch.Execute(null);
            }
        }

        private void SearchPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Focus();
        }

        private void MatchItemGrid_OnTap(object sender, GestureEventArgs e)
        {
            var vm = LayoutRoot.DataContext as SearchPage_Model;
            if (vm == null)
            {
                return;
            }

            vm.CommandNavigateToDetailPage.Execute(null);
        }

        private void LongListSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LayoutRoot == null)
            {
                return;
            }

            var vm = LayoutRoot.DataContext as SearchPage_Model;
            var control = sender as LongListSelector;
            if (vm == null || control == null)
            {
                return;
            }

            vm.SelectedSearchResult = (ListItem_Model)control.SelectedItem;
            vm.CommandNavigateToDetailPage.Execute(null);
        }
    }
}




