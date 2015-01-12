using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace WikiaWP
{
    public static class AppCache
    {
        public static async Task<IDictionary<string, string>> LoadCachedDictionaryAsync(string fileName)
        {
            var local = IsolatedStorageFile.GetUserStoreForApplication();
            if (!local.FileExists(fileName))
            {
                return null;
            }
            using (var stream = new IsolatedStorageFileStream(fileName, FileMode.Open, local))
            {
                using (var sr = new StreamReader(stream))
                {
                    var json = await sr.ReadToEndAsync();
                    if (!string.IsNullOrEmpty(json))
                    {
                        return JsonConvert.DeserializeObject<IDictionary<string, string>>(json);
                    }
                }
            }
            return null;
        }

        public static async Task SaveToCachedDictionaryAsync(IDictionary<string, string> dict, string fileName)
        {
            using (var local = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!local.DirectoryExists("cache"))
                {
                    local.CreateDirectory("cache");
                }

                using (var stream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, local))
                {
                    using (var sw = new StreamWriter(stream))
                    {
                        var json = JsonConvert.SerializeObject(dict);
                        sw.WriteAsync(json);
                    }
                }
            }
        }
    }
}