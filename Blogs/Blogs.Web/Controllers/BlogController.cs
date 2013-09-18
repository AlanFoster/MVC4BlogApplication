using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogs.Domain;
using Blogs.Web.Models;

namespace Blogs.Web.Controllers
{
    // Allow specific users [Authorize(Users = "foo, bar")]
    // allow specific roles [Authorize(Roles = "foo, bar")]
    [Authorize(Roles = "admin")]
    public class BlogController : Controller
    {
        private readonly IBlogDataSource _dataSource;

        public BlogController(IBlogDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        [AllowAnonymous]
        public ActionResult Detail(int id)
        {
            var blog = _dataSource.Blogs.First(b => b.Id == id);
            return View(blog);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new BlogViewModel();
            return View(model);
        }


    }
}
