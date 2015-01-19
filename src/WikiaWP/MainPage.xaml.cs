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

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

using WikiaWP.Models;
using WikiaWP.ViewModels;

namespace WikiaWP
{
    public partial class MainPage : MVVMPage
    {
        public MainPage()
            : base(null)
        {
            InitializeComponent();
        }

        public MainPage(MainPage_Model model)
            : base(model)
        {
            InitializeComponent();
        }

        private void SearchApplicationBarIconButton_OnClick(object sender, EventArgs e)
        {
            var vm = LayoutRoot.DataContext as MainPage_Model;
            if (vm == null)
            {
                return;
            }

            vm.CommandNavigateToSearch.Execute(null);
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

        private void AboutApplicationBarMenuItem_OnClick(object sender, EventArgs e)
        {
            var vm = LayoutRoot.DataContext as MainPage_Model;
            if (vm == null)
            {
                return;
            }

            vm.CommandNavigateToAbout.Execute(null);
        }

        private void PanoramaItem1_OnLoaded(object sender, RoutedEventArgs e)
        {
            var vm = LayoutRoot.DataContext as MainPage_Model;
            if (vm == null)
            {
                return;
            }

            vm.CommandLoadList1.Execute(null);
        }

        private void PanoramaItem2_OnLoaded(object sender, RoutedEventArgs e)
        {
            var vm = LayoutRoot.DataContext as MainPage_Model;
            if (vm == null)
            {
                return;
            }

            vm.CommandLoadList2.Execute(null);
        }

        private void PanoramaItem3_OnLoaded(object sender, RoutedEventArgs e)
        {
            var vm = LayoutRoot.DataContext as MainPage_Model;
            if (vm == null)
            {
                return;
            }

            vm.CommandLoadList3.Execute(null);
        }

        private void ListListSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var control = sender as LongListSelector;
            var vm = LayoutRoot.DataContext as MainPage_Model;
            if (control == null || vm == null || control.SelectedItem == null)
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
            if (item.Link.StartsWith("http"))
            {
                var task = new WebBrowserTask();
                task.Uri = new Uri(item.Link, UriKind.Absolute);
                task.Show();
            }
            else
            {
                vm.CommandNavigateToSelected.Execute(null);
            }
        }
    }
}
