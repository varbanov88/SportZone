using Microsoft.AspNetCore.Mvc;
using SportZone.Services.Admin;
using SportZone.Services.Newz;
using SportZone.Web.Areas.Admin.Models.News;
using SportZone.Web.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace SportZone.Web.Areas.Admin.Controllers
{
    public class NewsController : AdminBaseController
    {
        #region fields
        private readonly IAdminNewsService news;
        private readonly INewsService service;
        #endregion

        #region ctror
        public NewsController(IAdminNewsService news, INewsService service)
        {
            this.news = news;
            this.service = service;
        }
        #endregion

        #region methods

        public async Task<IActionResult> Index(string searchText, int page = 1)
        {
            var search = string.IsNullOrEmpty(searchText)
                 ? string.Empty
                 : searchText.ToLower();
            
            if (!string.IsNullOrEmpty(searchText))
            {
                ViewData["Title"] = $"Search Results For {search}";
            }

            else
            {
                ViewData["Title"] = "News Administration";
            }

            var viewModel = new NewsListingViewModel
            {
                News = await this.news.AllAsync(search, page),
                TotalNews = await this.news.TotalAsync(search),
                CurrentPage = page
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await this.service.GetByIdAsync(id);
            if (news == null)
            {
                return BadRequest();
            }

            await this.service.DeleteAsync(id);
            TempData.AddSuccessMessage($"{news.Title} deleted");

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
