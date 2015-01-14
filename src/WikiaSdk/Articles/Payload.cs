using System.Collections.Generic;

using LianZhao.Patterns.Composite;

namespace Wikia.Articles
{
    public class Payload : IComposite<Comment>
    {
        public Comment[] comments { get; set; }
        public Dictionary<string, User> users { get; set; }

        ICollection<Comment> IComposite<Comment>.Items
        {
            get
            {
                return comments;
            }
        }
    }
}