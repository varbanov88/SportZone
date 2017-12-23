using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportZone.Data.Models;
using SportZone.Services.Html;
using SportZone.Services.Newz;
using SportZone.Services.Newz.Models;
using SportZone.Web.Areas.News.Models;
using SportZone.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static SportZone.Web.WebConstants;

namespace SportZone.Web.Areas.News.Controllers
{
    public class ReportersController : ReportersBaseController
    {
        #region fields

        private readonly INewsService news;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IHtmlService html;

        #endregion

        #region ctor

        public ReportersController(
            INewsService news, 
            UserManager<User> userManager, 
            IHtmlService html, 
            RoleManager<IdentityRole> roleManager)
            : base()
        {
            this.news = news;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.html = html;
        }

        #endregion

        #region methods
        public IActionResult Create()
                => View(new CreateNewsViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsViewModel model)
        {
            if (model.Image == null)
            {
                TempData.AddErrorMessage($"Please upload picture");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = this.userManager.GetUserId(User);
            model.Content = this.html.Sanitize(model.Content);
            var tags = this.FormatTags(model.Tags);
            await this.news.CreateAsync(userId, model.Image, model.Title, model.Content, model.VideoUrl, tags);

            return RedirectToAction(IndexIActionResult, NewsControllerName);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var news = await this.news.GetByIdAsync(id);
            if (news == null)
            {
                return BadRequest();
            }

            if (!await CanUserAccessNews(news))
            {
                return BadRequest();
            }

            var tags = news.Tags.Select(t => t.Tag.Content).ToList();
            var viewModel = new EditNewsViewModel
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.Content,
                ImageByte = news.Image,
                VideoUrl = news.VideoUrl,
                Tags = string.Join(", ", tags)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditNewsViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var news = await this.news.GetByIdAsync(id);
            if (news == null)
            {
                return BadRequest();
            }

            if (!await CanUserAccessNews(news))
            {
                return BadRequest();
            }

            model.Content = this.html.Sanitize(model.Content);
            var tags = this.FormatTags(model.Tags);
            await this.news.EditAsync(id, model.Image, model.Title, model.Content, model.VideoUrl, tags);

            return RedirectToAction(IndexIActionResult, NewsControllerName);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await this.news.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            if (!await CanUserAccessNews(news))
            {
                return BadRequest();
            }

            await this.news.DeleteAsync(id);
            TempData.AddSuccessMessage($"News {news.Title} successfully deleted");

            return RedirectToAction("Index", "NewsZone");
        }

        private HashSet<string> FormatTags(string tags)
        {
            var inputTags = tags
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim());

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

        private async Task<bool> CanUserAccessNews(NewsDetailsServiceModel news)
        {
            var user = await this.userManager.GetUserAsync(User);
            var userIsInAdminRole = await this.userManager.IsInRoleAsync(user, AdministratorRole);
            if (news.AuthorId != user.Id)
            {
                if (!userIsInAdminRole)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}