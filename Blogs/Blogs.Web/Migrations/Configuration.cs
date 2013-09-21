using System.Collections.Generic;
using System.Web.Security;
using Blogs.Domain;
using Blogs.Web.infrastructure;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;
using WebMatrix.WebData;

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
            // AutomaticMigrationDataLossAllowed = false;
        }

        // Initially populates the database with static data
        protected override void Seed(Blogs.Web.infrastructure.BlogDb context)
        {
            SeedMembership();

            var admin = context.Users.First(_ => _.UserName == "alan");
            var guest = context.Users.First(_ => _.UserName == "guest");

            SeedBlogs(context, admin, guest);

            context.SaveChanges();
        }

        private void SeedMembership()
        {
            WebSecurity
                .InitializeDatabaseConnection("DefaultConnection", "User", "Id", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider) Roles.Provider;
            var membership = (SimpleMembershipProvider) Membership.Provider;

            // Create Admin account for alan username
            roles.CreateRoleIdempotent("Admin");

            membership.CreateUserIdempotent("alan", "password");
            membership.CreateUserIdempotent("guest", "password");     
       
            roles.AddRolesIdempotent("alan", "Admin");
        }

        private static void SeedBlogs(BlogDb context, User admin, User guest)
        {
            // Generate blogs with comments
            Enumerable.Range(1, 100)
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

    static class MembershipExtension
    {
        public static void CreateRoleIdempotent(this SimpleRoleProvider roleProvider,
            string roleName)
        {
            if (!roleProvider.RoleExists(roleName))
            {
                roleProvider.CreateRole(roleName);
            }
        }

        public static void CreateUserIdempotent(this SimpleMembershipProvider membershipProvider,
            string username, string password)
        {
            if (membershipProvider.GetUser(username, false) == null)
            {
                membershipProvider.CreateUserAndAccount(username, password);
            }
        }

        public static void AddRolesIdempotent(this SimpleRoleProvider roleProvider,
            string username, params string[] roles)
        {
            var userRoles = roleProvider.GetRolesForUser(username);
            foreach (var role in roles.Where(role => !userRoles.Contains(role)))
            {
                roleProvider.AddUsersToRoles(new[] { username }, new[] { role });
            }
        }
    }
}