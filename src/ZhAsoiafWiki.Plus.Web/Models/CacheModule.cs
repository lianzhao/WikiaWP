using System;

namespace ZhAsoiafWiki.Plus.Web.Models
{
    [Flags]
    public enum CacheModule
    {
        None = 0,
        EnZhDictionary = 1,
        RedirectDictionary = 2,
        Articles = 4,
        All = EnZhDictionary | RedirectDictionary | Articles,
    }
}