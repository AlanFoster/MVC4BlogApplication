using System.Data.Entity;
using System.Linq;
using Blogs.Domain;

namespace Blogs.Web.infrastructure
{
    public class BlogDb : DbContext, IBlogDataSource
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        public BlogDb()
            : base("DefaultConnection")
        {
        }

        IQueryable<User> IBlogDataSource.Users
        {
            get { return Users; }
        }

        IQueryable<Blog> IBlogDataSource.Blogs
        {
            get { return Blogs; }
        }

        public void Save()
        {
            SaveChanges();
        }

    }
}