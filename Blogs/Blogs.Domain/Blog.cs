using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain
{
    public class Blog
    {
        public virtual int Id { get; set; }
        public virtual User Poster { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual List<Comment> Comments { get; set; } 
    }
}
