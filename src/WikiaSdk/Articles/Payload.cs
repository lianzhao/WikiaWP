using System.Collections.Generic;

namespace Wikia.Articles
{
    public class Payload
    {
        public Comment[] comments { get; set; }
        public Dictionary<string, User> users { get; set; }
    }
}