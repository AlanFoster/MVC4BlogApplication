using System.Collections.Generic;
using Blogs.Domain;
using WebGrease.Css.Extensions;

namespace Blogs.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// Called initially with `enable-migrations` to create this configuration file.
    /// Your seed method can be called with `update-database`.
    /// 
    /// By default this will run against localdb, which will go under App_Data *.mdf.
    /// To change this you will need to overwrite the default connection string.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<Blogs.Web.infrastructure.BlogDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        // Initially populates the database with static data
        protected override void Seed(Blogs.Web.infrastructure.BlogDb context)
        {
            context.Users.AddOrUpdate(u => u.Name,
                new User {Name = "Admin"},
                new User {Name = "Guest"}
                );

            context.SaveChanges();

            var admin = context.Users.First(_ => _.Name == "Admin");
            var guest = context.Users.First(_ => _.Name == "Guest");

            // Generate 5 blogs with comments
            Enumerable.Range(1, 5)
                .Select(blogId =>
                {
                    var blog = new Blog
                    {
                        Poster = admin,
                        Title = string.Format("Hello World #{0}", blogId),
                        Content = string.Format("Content for Blog {0}", blogId)
                    };

                    // Generate comments
                    blog.Comments = Enumerable.Range(1, 3)
                            .Select(commentId => new Comment
                            {
                                Poster = guest,
                                Title = string.Format("Guest Post #{0}", commentId),
                                Content = string.Format("Guest Content #{0} For Post #{1}", commentId, blogId),
                                Blog = blog
                            }).ToList();

                    return blog;
                })
                .ForEach(blog => context.Blogs.AddOrUpdate(b => b.Title, blog));
        }
    }
}