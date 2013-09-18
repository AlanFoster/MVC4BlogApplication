using Blogs.Domain;

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
            var admin = new User {Name = "Admin"};
            var guest = new User {Name = "Guest"};

            context.Users.AddOrUpdate(u => u.Name,
                    admin,
                    guest
           );

            context.Blogs.AddOrUpdate(b => b.Title, 
                new Blog { Poster = admin, Title = "Hello World 1", Content = "My First Post"},
                new Blog { Poster = admin, Title = "Hello World 2", Content = "My Second Post"},
                new Blog { Poster = admin, Title = "Hello World 3", Content = "My Third Post"},
                new Blog { Poster = admin, Title = "Hello World 4", Content = "My Fourth Post"}
            );

        }
    }
}