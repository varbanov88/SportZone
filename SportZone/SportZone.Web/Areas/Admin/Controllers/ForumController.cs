using Microsoft.AspNetCore.Mvc;
using SportZone.Services.Admin;
using SportZone.Services.Forum;
using SportZone.Web.Areas.Admin.Models.Forum;
using System.Threading.Tasks;

namespace SportZone.Web.Areas.Admin.Controllers
{
    public class ForumController : AdminBaseController
    {
        #region fields
        private readonly IAdminForumService forum;
        private readonly IForumService service;
        #endregion

        #region ctor
        public ForumController(IAdminForumService forum, IForumService service)
        {
            this.forum = forum;
            this.service = service;
        }
        #endregion

        #region methods
        public async Task<IActionResult> Index(string searchText, int page = 1)
        {
            var search = string.IsNullOrEmpty(searchText)
                 ? string.Empty
                 : searchText.ToLower();


            var viewModel = new ArticleListingViewModel
            {
                Articles = await this.forum.AllAsync(search, page),
                TotalArticles = await this.forum.TotalAsync(search),
                CurrentPage = page
            };

            if (!string.IsNullOrEmpty(searchText))
            {
                ViewData["Title"] = $"Search Results For {searchText}";
            }

            else
            {
                ViewData["Title"] = "Forum Administration";
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await this.service.GetByIdAsync(id);
            if (article == null)
            {
                return BadRequest();
            }

            await this.forum.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}