using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportZone.Data.Models;
using SportZone.Services.Forum;
using SportZone.Web.Areas.Forum.Models;
using SportZone.Web.Infrastructure.Extensions;
using System;
using System.Threading.Tasks;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class ArticlesController : Controller
    {
        #region fields

        private readonly IForumService articles;
        private readonly UserManager<User> userManager;

        #endregion

        #region ctor

        public ArticlesController(IForumService articles, UserManager<User> userManager)
        {
            this.articles = articles;
            this.userManager = userManager;
        }

        #endregion

        #region methods
        public async Task<IActionResult> Index(string searchText, int page = 1)
        {
            var viewModel = new ArticleListingViewModel
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
            => View(new CreateArticleViewModel());

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

        public async Task<IActionResult> Details(int id, int page = 1)
        {
            var article = await this.articles.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            var viewModel = new CommentViewModel
            {
                ArticleId = id,
                ArticleTitle = article.Title,
                Content = article.Content,
                Comments = await this.articles.GetCommentsAsync(id, page),
                TotalComments = await this.articles.TotalCommentsAsync(id),
                CurrentPage = page
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Comment(int id, string comment)
        {
            var article = await this.articles.GetByIdAsync(id);
            if (article == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(comment) || comment.Length < 5 || comment.Length > 200)
            {
                TempData.AddErrorMessage(CommentTextLengthErrorText);
                return RedirectToAction(nameof(Details), new { id = id });
            }

            var userId = this.userManager.GetUserId(User);
            await this.articles.AddCommentAsync(id, comment, userId);
            TempData.AddSuccessMessage($"Comment successfully added to {article.Title} news");
            var lastPage = (int)Math.Ceiling((double)await this.articles.TotalCommentsAsync(id) / CommentPageSize);

            return RedirectToAction(nameof(Details), new { id = id, page = lastPage });
        }

        public async Task<IActionResult> Search(string searchText, int page = 1)
        {
            var viewModel = new ArticleListingViewModel
            {
                Articles = await this.articles.AllArticlesAsync(searchText, page),
                TotalArticles = await this.articles.TotalAsync(searchText),
                CurrentPage = page
            };

            ViewData["Title"] = $"Search Results For {searchText}";

            return View(nameof(Index), viewModel);
        }

        #endregion
    }
}