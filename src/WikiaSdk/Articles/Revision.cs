using System;

using LianZhao;

namespace Wikia.Articles
{
    public class Revision
    {
        public int id { get; set; }
        public string user { get; set; }
        public int user_id { get; set; }
        public int timestamp { get; set; }

        public DateTime UploadedUtc
        {
            get
            {
                return DateTimeExtensions.FromUnixTimeStamp(timestamp);
            }
        }
    }
}