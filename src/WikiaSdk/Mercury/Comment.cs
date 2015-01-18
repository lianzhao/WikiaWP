using System;
using System.Collections.Generic;

using LianZhao;
using LianZhao.Patterns.Composite;

namespace Wikia.Mercury
{
    public class Comment : IComposite<Comment>
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
                return DateTimeExtensions.FromUnixTimeStamp(created);
            }
        }

        ICollection<Comment> IComposite<Comment>.Items
        {
            get
            {
                return comments;
            }
        }
    }
}