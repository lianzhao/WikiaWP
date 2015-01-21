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

using LianZhao.Patterns.Func;

namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static Action SearchPageConfig =
            CreateAndAddToAllConfig(ConfigSearchPage);

        public static void ConfigSearchPage()
        {
            ViewModelLocator<SearchPage_Model>.Instance.Register(
                context => new SearchPage_Model()
                               {
                                   SearchTextMappingFunc = from =>
                                       {
                                           // todo introduce ioc?
                                           // create new instance per invoking
                                           var dictFun1 = new WikiDictTryFunc(ApiClient.MainDictionary);
                                           var dictFun2 = new WikiDictTryFunc(ApiClient.RedirectDictionary);
                                           var func = new CompositeTryFunc<string>(dictFun1, dictFun2);
                                           return func.ToDelegate().Invoke(from);
                                       }
                               }).GetViewMapper().MapToDefault<SearchPage>();

        }
    }
}
