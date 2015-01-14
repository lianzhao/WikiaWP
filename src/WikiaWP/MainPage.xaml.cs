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

using Microsoft.Phone.Shell;

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
    }
}
