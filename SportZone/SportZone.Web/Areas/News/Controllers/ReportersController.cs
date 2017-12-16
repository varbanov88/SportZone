using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportZone.Data.Models;
using SportZone.Services.Newz;
using SportZone.Web.Areas.News.Models;
using SportZone.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
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
            :base()
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
            var tags = this.FormatTags(model.Tags);
            await this.news.CreateAsync(userId, model.Image, model.Title, model.Content, model.VideoUrl, tags);

            return RedirectToAction(IndexIActionResult, NewsControllerName);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var news = await this.news.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            await this.news.DeleteAsync(id);
            TempData.AddSuccessMessage($"News {news.Title} successfully deleted");

            return RedirectToAction("Index", "NewsZone");
        }

        private HashSet<string> FormatTags(string tags)
        {
            var inputTags = tags
                .Split(new[] { ','}, StringSplitOptions.RemoveEmptyEntries);
            var result = new HashSet<string>();

            foreach (var tag in inputTags)
            {
                var tagToAdd = tag.ValidateTag();
                if (!string.IsNullOrEmpty(tagToAdd))
                {
                    result.Add(tagToAdd);
                }                
            }

            return result;
        }
        #endregion
    }
}