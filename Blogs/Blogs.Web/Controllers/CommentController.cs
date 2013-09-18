using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogs.Domain;
using Blogs.Web.Models;

namespace Blogs.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly IBlogDataSource _dataSource;

        public CommentController(IBlogDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        [HttpGet]
        public ActionResult Create(int blogId)
        {
            var model = new CreateCommentViewModel
            {
                BlogId = blogId,
                // Hardcode all comments to be added by guests for now
                PosterId = 2
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateCommentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var blog = _dataSource.Blogs.Single(b => b.Id == viewModel.BlogId);
                var poster = _dataSource.Users.Single(u => u.Id == viewModel.PosterId);

                var comment = new Comment
                {
                    Title = viewModel.Title,
                    Content = viewModel.Content,
                    Poster = poster,
                    Blog = blog
                };
                blog.Comments.Add(comment);

                _dataSource.Save();

                return RedirectToAction("detail", "Blog", new {id = viewModel.BlogId});
            }
            return View(viewModel);
        }

    }
}
