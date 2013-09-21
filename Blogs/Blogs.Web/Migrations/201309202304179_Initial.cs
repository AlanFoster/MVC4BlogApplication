namespace Blogs.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Generated with `Add-Migration name`
    /// Applied with `Update-database`
    /// </summary>
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        PostDate = c.DateTime(storeType: "date", defaultValueSql: "GETDATE()"),
                        Poster_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Poster_Id)
                .Index(t => t.Poster_Id);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        PostDate = c.DateTime(storeType: "date", defaultValueSql: "GETDATE()"),
                        Poster_Id = c.Int(),
                        Blog_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Poster_Id)
                .ForeignKey("dbo.Blog", t => t.Blog_Id)
                .Index(t => t.Poster_Id)
                .Index(t => t.Blog_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comment", new[] { "Blog_Id" });
            DropIndex("dbo.Comment", new[] { "Poster_Id" });
            DropIndex("dbo.Blog", new[] { "Poster_Id" });
            DropForeignKey("dbo.Comment", "Blog_Id", "dbo.Blog");
            DropForeignKey("dbo.Comment", "Poster_Id", "dbo.User");
            DropForeignKey("dbo.Blog", "Poster_Id", "dbo.User");
            DropTable("dbo.Comment");
            DropTable("dbo.Blog");
            DropTable("dbo.User");
        }
    }
}
