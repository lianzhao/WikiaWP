using System;

using MVVMSidekick.Views;

using WikiaWP;
using WikiaWP.ViewModels;

namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static Action SearchPageConfig =
            CreateAndAddToAllConfig(ConfigSearchPage);

        public static void ConfigSearchPage()
        {
            ViewModelLocator<SearchPage_Model>
                .Instance
                .Register(context =>
                    new SearchPage_Model())
                .GetViewMapper()
                .MapToDefault<SearchPage>();

        }
    }
}
