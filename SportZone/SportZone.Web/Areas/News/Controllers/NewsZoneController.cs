using Microsoft.AspNetCore.Mvc;
using SportZone.Services.Newz;
using SportZone.Web.Areas.News.Models;
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

        #endregion

        #region ctror

        public NewsZoneController(INewsService news, ITagService tags)
        {
            this.news = news;
            this.tags = tags;
        }

        #endregion

        #region methods

        public async Task<IActionResult> Index(int page = 1)
        {
            var viewModel = new NewsListingViewModel
            {
                Articles = await this.news.AllAsync(string.Empty, page),
                TotalNews = await this.news.TotalAsync(string.Empty),
                CurrentPage = page
            };

            ViewData["Title"] = $"All News - Page {page}";

            return View(viewModel);
        }
           
        public async Task<IActionResult> Details(int id)
        {
            var news = await this.news.GetByIdAsync(id);
            if (news.VideoUrl != null)
            {
                news.VideoUrl = VideoUrlPrefix + news.VideoUrl;
            }
            return View(news);
        }

        public async Task<IActionResult> Search(string searchText, int page = 1)
        {
            var viewModel = new NewsListingViewModel
            {
                Articles = await this.news.AllAsync(searchText, page),
                TotalNews = await this.news.TotalAsync(searchText),
                CurrentPage = page
            };

            ViewData["Title"] = $"Search Results For {searchText}";

            return View(viewModel);
        }

        public IActionResult SearchByTag(int tagId, int page = 1)
        {
            var tag = this.tags.GetName(tagId);
            var news = tags.All(tagId, page);

            ViewData["Title"] = $"News with {tag} tag";

            var viewModel = new NewsListingViewModel
            {
                Articles = this.tags.All(tagId, page),
                TotalNews = this.tags.TotalNews(tagId),
                CurrentPage = page
            };

            return View(nameof(Search), viewModel);
        }
        #endregion
    }
}