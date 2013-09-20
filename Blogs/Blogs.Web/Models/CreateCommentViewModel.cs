using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogs.Web.Models
{
    // Model which is specific to this state view
    public class CreateCommentViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int BlogId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int PosterId { get; set; }
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}