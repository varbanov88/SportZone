using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportZone.Data.Models;
using SportZone.Services.Forum;
using SportZone.Services.Forum.Models;
using SportZone.Web.Areas.Forum.Models;
using SportZone.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class ArticlesController : Controller
    {
        private readonly IForumService articles;
        private readonly UserManager<User> userManager;

        public ArticlesController(IForumService articles, UserManager<User> userManager)
        {
            this.articles = articles;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var viewModel = new ArticleListingModel
            {
                Articles = await this.articles.AllArticlesAsync(string.Empty, page),
                TotalArticles = await this.articles.TotalAsync(string.Empty),
                CurrentPage = page
            };

            ViewData["Title"] = $"All Articles - Page {page}";

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new CreateArticleViewModel());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = this.userManager.GetUserId(User);

            await this.articles.CreateAsync(model.Title, model.Content, userId);

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
                    => this.ViewOrNotFound(await this.articles.GetByIdAsync(id));

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Comment(int id, string comment)//article id
        {
            var article = await this.articles.GetByIdAsync(id);
            if (article == null)
            {
                return BadRequest();
            }

            if (comment.Length < 5 || comment.Length > 200)
            {
                TempData.AddErrorMessage(CommentTextLengthErrorText);
                return RedirectToAction(nameof(Details), new { id = id});
            }

            var userId = this.userManager.GetUserId(User);
            await this.articles.AddCommentAsync(id, comment, userId);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> Search(string searchText, int page = 1)
        {
            var viewModel = new ArticleListingModel
            {
                Articles = await this.articles.AllArticlesAsync(searchText , page),
                TotalArticles = await this.articles.TotalAsync(searchText),
                CurrentPage = page
            };

            ViewData["Title"] = $"Search Results For {searchText}";

            return View(viewModel);
        }
    }
}