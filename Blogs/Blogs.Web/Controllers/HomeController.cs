using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogs.Domain;
using Blogs.Web.Models;

namespace Blogs.Web.Controllers
{
    public class HomeController : Controller
    {
        private IBlogDataSource _dataSource;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource">Injected datasource configured via DependencyResolution/StructureMapDependencyResolver</param>
        public HomeController(IBlogDataSource dataSource)
        {
            this._dataSource = dataSource;
        }

        public ActionResult Index(string searchTerm = null)
        {
            var blogs = _dataSource.Blogs
                .Where(blog => searchTerm == null || new[] {blog.Content, blog.Title}.Any(_ => _.Contains(searchTerm)))
                .Take(10)
                .Select(_ => new BlogListViewModel
                {
                    Id = _.Id,
                    Poster = _.Poster,
                    Title = _.Title,
                    Content = _.Content,
                    CommentCount = _.Comments.Count()
                });

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Blogs", blogs);
            }

            return View(blogs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
