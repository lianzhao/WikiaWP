using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using WikiaWP;
using WikiaWP.ViewModels;
using System;
using System.Net;
using System.Windows;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static Action FileDetailPageConfig =
            CreateAndAddToAllConfig(ConfigFileDetailPage);

        public static void ConfigFileDetailPage()
        {
            ViewModelLocator<FileDetailPage_Model>
                .Instance
                .Register(context =>
                    new FileDetailPage_Model())
                .GetViewMapper()
                .MapToDefault<FileDetailPage>();

        }
    }
}
