using System;
using System.Collections.Generic;

using LianZhao.Collections.Generic;
using LianZhao.Patterns.Func;

namespace WikiaWP
{
    public class WikiDictTryFunc : ITryFunc<string, string>
    {
        private readonly IDictionary<string, string> _dict;

        public WikiDictTryFunc(IDictionary<string, string> dict)
        {
            _dict = dict;
        }

        public bool TryInvoke(string @from, out string to)
        {
            if (_dict != null)
            {
                return _dict.TryGetValue(from, out to, StringComparison.OrdinalIgnoreCase);
            }

            to = from;
            return false;
        }
    }
}