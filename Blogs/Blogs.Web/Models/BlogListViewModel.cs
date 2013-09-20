using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blogs.Domain;

namespace Blogs.Web.Models
{
    public class BlogListViewModel
    {
        public int Id { get; set; }
        public User Poster { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CommentCount { get; set; }
        public DateTime? PostDate { get; set; }
    }
}