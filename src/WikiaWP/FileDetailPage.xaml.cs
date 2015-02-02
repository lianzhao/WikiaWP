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

using Microsoft.Phone.Shell;

using WikiaWP;
using WikiaWP.ViewModels;


namespace WikiaWP
{
    public partial class FileDetailPage : MVVMPage
    {



        public FileDetailPage()
            : base(null)
        {
            this.InitializeComponent();
        }
        public FileDetailPage(FileDetailPage_Model model)
            : base(model)
        {
            this.InitializeComponent();
        }

        private void Image_OnImageOpened(object sender, RoutedEventArgs e)
        {
            ProgressBar.Visibility = Visibility.Collapsed;
        }

        private void ApplicationBarMenuItem_OnClick(object sender, EventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            Pivot.Visibility = Visibility.Visible;
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

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (Image.Visibility == Visibility.Visible)
            {
                base.OnBackKeyPress(e);
            }
            else
            {
                Image.Visibility = Visibility.Visible;
                Pivot.Visibility = Visibility.Collapsed;
                e.Cancel = true;
            }
        }
    }
}




