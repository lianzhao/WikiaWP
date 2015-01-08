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
using WikiaWP;
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
                vm.CommandSearch.Execute(null);
            }
        }

        private void SearchPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Focus();
        }
    }
}




