using System.Collections.Generic;

namespace Wikia.Mercury
{
    public class Article
    {
        public string content { get; set; }
        public object[] media { get; set; }
        public IDictionary<string, User> users { get; set; }
        public Category[] categories { get; set; }
        public string description { get; set; }
    }
}