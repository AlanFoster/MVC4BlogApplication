using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
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
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<>);
        }

        /**
         * Code first fluent model builder
         * 
         *   protected override void OnModelCreating(DbModelBuilder modelBuilder)
         *   {
         *       var blog = modelBuilder.Entity<Blog>()
         *
         *       blog.HasKey(b => b.Id);
         *       blog.Property(b => b.Title).HasMaxLength(20)
         *   }
        */

/*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
/*            modelBuilder.Entity<Blog>()
                .Property(_ => _.PostDate)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);#1#

            // modelBuilder.Configurations.Add(new CustomMapper());
            // public class CustomMapper : EntityTypeConfiguration<Foo> { ctor { ... has... } };
            base.OnModelCreating(modelBuilder);
        }
*/


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