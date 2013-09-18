using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogs.Domain;

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

        public ActionResult Index()
        {
            var blogs = _dataSource.Blogs;

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
