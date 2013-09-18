using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain
{
    public class Comment
    {
        public virtual int Id { get; set; }
        public virtual User Poster { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime? PostDate { get; set; }
    }
}
