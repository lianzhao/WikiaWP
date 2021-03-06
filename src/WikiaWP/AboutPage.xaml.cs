﻿using System.Reactive;
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

using Microsoft.Phone.Tasks;

using WikiaWP;
using WikiaWP.ViewModels;


namespace WikiaWP
{
    public partial class AboutPage : MVVMPage
    {



        public AboutPage()
            : base(null)
        {
            this.InitializeComponent();
        }
        public AboutPage(AboutPage_Model model)
            : base(model)
        {
            this.InitializeComponent();
        }

        private void ThirdPartyNoticeHyperLinkButton_OnClick(object sender, RoutedEventArgs e)
        {
            var task = new WebBrowserTask();
            task.Uri = new Uri("https://github.com/lianzhao/WikiaWP/blob/master/ThirdPartyNotice.md", UriKind.Absolute);
            task.Show();
        }
    }
}




