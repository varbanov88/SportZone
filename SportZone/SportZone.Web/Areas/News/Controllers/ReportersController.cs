using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportZone.Data.Models;
using SportZone.Services.Newz;
using SportZone.Web.Areas.News.Models;
using System.Threading.Tasks;

using static SportZone.Web.WebConstants;

namespace SportZone.Web.Areas.News.Controllers
{
    public class ReportersController : ReportersBaseController
    {
        #region fields

        private readonly INewsService news;
        private readonly UserManager<User> userManager;

        #endregion

        #region ctor
        public ReportersController(INewsService news, UserManager<User> userManager)
        {
            this.news = news;
            this.userManager = userManager;
        }
        #endregion

        #region methods
        public IActionResult Create()
                => View(new CreateNewsViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }



            var userId = this.userManager.GetUserId(User);
            await this.news.CreateAsync(userId, model.Image, model.Title, model.Content, model.VideoUrl);

            return RedirectToAction(IndexIActionResult, NewsControllerName);
        }
        #endregion
    }
}