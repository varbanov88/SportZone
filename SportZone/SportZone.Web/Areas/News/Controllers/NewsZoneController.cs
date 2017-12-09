using Microsoft.AspNetCore.Mvc;
using SportZone.Services.Newz;
using SportZone.Web.Areas.News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var news = new NewsListingViewModel
            {
                Articles = await this.news.AllAsync()
            };

            return View(news);
        }
           
        public async Task<IActionResult> Details(int id)
        {
            return null;
        }
        #endregion
    }
}
