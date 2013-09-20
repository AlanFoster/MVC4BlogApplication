using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogs.Domain;
using Blogs.Web.Models;
using PagedList;

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

        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            var blogs = filterBlogs(_dataSource.Blogs, searchTerm)
                .Select(_ => new BlogListViewModel
                {
                    Id = _.Id,
                    Poster = _.Poster,
                    Title = _.Title,
                    Content = _.Content,
                    PostDate = _.PostDate,
                    CommentCount = _.Comments.Count()
                })
                .ToPagedList(page, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Blogs", blogs);
            }

            return View(blogs);
        }


        public ActionResult BlogSearch(string term)
        {
            var model = filterBlogs(_dataSource.Blogs, term)
                .Take(10)
                .Select(blog => new {label = blog.Title/*, value = blog.Content*/});

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private IQueryable<Blog> filterBlogs(IQueryable<Blog> blogs, string term)
        {
            return blogs
                .Where(blog => term == null || new[] {blog.Content, blog.Title}.Any(_ => _.Contains(term)))
                .OrderBy(_ => _.PostDate);
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
