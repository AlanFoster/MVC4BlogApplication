using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogs.Domain;

namespace Blogs.Web.Controllers
{
    public class BlogController : Controller
    {
        private IBlogDataSource _dataSource;

        public BlogController(IBlogDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public ActionResult Detail(int id)
        {
            var blog = _dataSource.Blogs.First(b => b.Id == id);
            return View(blog);
        }
    }
}
