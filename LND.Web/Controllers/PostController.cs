using AutoMapper;
using LND.Model.Models;
using LND.Service;
using LND.Web.Infrastructure.Core;
using LND.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LND.Web.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        private IPostCategoryService _postCategoryService;

        public PostController(IPostCategoryService postCategoryService)

        {
            this._postCategoryService = postCategoryService;
        }

        public ActionResult Index()
        {
            var postModel = _postCategoryService.GetAll();
            var postViewModel = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(postModel);
            return View(postViewModel);
        }
    }
}