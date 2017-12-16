﻿using Microsoft.AspNetCore.Mvc;
using SportZone.Services.Newz;
using SportZone.Web.Areas.News.Models;
using System.Linq;
using System.Threading.Tasks;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.News.Controllers
{
    [Area("News")]
    public class NewsZoneController : Controller
    {
        #region fields

        private readonly INewsService news;

        #endregion

        #region ctror

        public NewsZoneController(INewsService news)
        {
            this.news = news;
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
        #endregion
    }
}
