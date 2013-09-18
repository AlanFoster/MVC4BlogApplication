using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain
{
    public interface IBlogDataSource
    {
        IQueryable<Blog> Blogs { get; }
        IQueryable<User> Users { get; }
        void Save();
    }
}
