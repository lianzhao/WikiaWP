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
        static Action ArticleDetailPageConfig =
            CreateAndAddToAllConfig(ConfigArticleDetailPage);

        public static void ConfigArticleDetailPage()
        {
            ViewModelLocator<ArticleDetailPage_Model>
                .Instance
                .Register(context =>
                    new ArticleDetailPage_Model())
                .GetViewMapper()
                .MapToDefault<ArticleDetailPage>();

        }
    }
}
