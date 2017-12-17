using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportZone.Data.Models;
using SportZone.Services.Newz;
using SportZone.Web.Areas.News.Models;
using SportZone.Web.Infrastructure.Extensions;
using System.Threading.Tasks;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.News.Controllers
{
    [Area("News")]
    public class NewsZoneController : Controller
    {
        #region fields

        private readonly INewsService news;
        private readonly ITagService tags;
        private readonly UserManager<User> userManager;

        #endregion

        #region ctror

        public NewsZoneController(INewsService news, ITagService tags, UserManager<User> userManager)
        {
            this.news = news;
            this.tags = tags;
            this.userManager = userManager;
        }

        #endregion

        #region methods

        public async Task<IActionResult> Index(int page = 1)
        {
            var tabs = new NewsTabsViewModel
            {
                LatestNews = await this.news.LatestAsync(),
                MostReadNews = await this.news.MostReadAsync()
            };

            var viewModel = new NewsListingViewModel
            {
                Articles = await this.news.AllAsync(string.Empty, page),
                Tags = await this.tags.GetPopularAsync(),
                NewsTabs = tabs,
                TotalNews = await this.news.TotalAsync(string.Empty),
                CurrentPage = page
            };

            ViewData["Title"] = $"All News - Page {page}";

            return View(viewModel);
        }
           
        public async Task<IActionResult> Details(int id)
        {
            var news = await this.news.GetByIdAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            var tabs = new NewsTabsViewModel
            {
                LatestNews = await this.news.LatestAsync(),
                MostReadNews = await this.news.MostReadAsync()
            };

            var article = new NewsDetailsViewModel
            {
                News = news,
                NewsTabs = tabs,
                Tags = await this.tags.GetPopularAsync()
            };

            if (article.News.VideoUrl != null)
            {
                article.News.VideoUrl = VideoUrlPrefix + article.News.VideoUrl;
            }
            await this.news.ReadAsync(id);
            return View(article);
        }

        public async Task<IActionResult> Search(string searchText, int page = 1)
        {
            var tabs = new NewsTabsViewModel
            {
                LatestNews = await this.news.LatestAsync(),
                MostReadNews = await this.news.MostReadAsync()
            };

            var viewModel = new NewsListingViewModel
            {
                Articles = await this.news.AllAsync(searchText, page),
                TotalNews = await this.news.TotalAsync(searchText),
                NewsTabs = tabs,
                Tags = await this.tags.GetPopularAsync(),
                CurrentPage = page
            };

            ViewData["Title"] = $"Search Results For {searchText}";

            return View(viewModel);
        }

        public async Task<IActionResult> SearchByTag(int tagId, int page = 1)
        {
            var tag = this.tags.GetName(tagId);
            //var news = tags.All(tagId, page);
            var tabs = new NewsTabsViewModel
            {
                LatestNews = await this.news.LatestAsync(),
                MostReadNews = await this.news.MostReadAsync()
            };

            ViewData["Title"] = $"News with {tag} tag";

            var viewModel = new NewsListingViewModel
            {
                Articles = this.tags.All(tagId, page),
                TotalNews = this.tags.TotalNews(tagId),
                NewsTabs = tabs,
                Tags = await this.tags.GetPopularAsync(),
                CurrentPage = page
            };

            return View(nameof(Search), viewModel);
        }

        public async Task<IActionResult> Comment(int id, string comment)
        {
            var news = await this.news.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            if (comment.Length < 5 || comment.Length > 200)
            {
                TempData.AddErrorMessage(CommentTextLengthErrorText);
                return RedirectToAction(nameof(Details), new { id = id });
            }

            var userId = this.userManager.GetUserId(User);
            await this.news.AddCommentAsync(id, comment, userId);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        #endregion
    }
}