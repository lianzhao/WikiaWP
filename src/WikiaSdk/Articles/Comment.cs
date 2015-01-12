using System;

namespace Wikia.Articles
{
    public class Comment
    {
        public int id { get; set; }
        public string text { get; set; }
        public int created { get; set; }
        public string userName { get; set; }
        public Comment[] comments { get; set; }

        public DateTime CreatedUtc
        {
            get
            {
                return DateTime.UtcNow.AddSeconds(-created);
            }
        }
    }
}