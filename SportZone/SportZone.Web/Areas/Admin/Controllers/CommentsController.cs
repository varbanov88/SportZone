using Microsoft.AspNetCore.Mvc;
using SportZone.Services.Admin;
using SportZone.Web.Areas.Admin.Models.Comments;
using SportZone.Web.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace SportZone.Web.Areas.Admin.Controllers
{
    public class CommentsController : AdminBaseController
    {
        #region fields

        private readonly IAdminCommentService comments;

        #endregion

        #region ctor

        public CommentsController(IAdminCommentService comments)
        {
            this.comments = comments;
        }

        #endregion

        #region methods

        public async Task<IActionResult> ForumComments(string searchText, int page = 1)
        {
            var viewModel = new ForumCommentsListingViewModel
            {
                Comments = await this.comments.AllForumAsync(searchText, page),
                TotalComments = await this.comments.TotalForumAsync(),
                TotalNewsComments = await this.comments.TotalNewsAsync(),
                CurrentPage = page
            };

            if (!string.IsNullOrEmpty(searchText))
            {
                ViewData["Title"] = $"Search Results For {searchText}";
            }

            else
            {
                ViewData["Title"] = "Delete only abusive or inappropriate comments!";
            }

            return View(viewModel);
        }

        public async Task<IActionResult> NewsComments(string searchText, int page = 1)
        {
            var viewModel = new NewsCommentsListingViewModel
            {
                Comments = await this.comments.AllNewsAsync(searchText, page),
                TotalComments = await this.comments.TotalNewsAsync(),
                TotalForumComments = await this.comments.TotalForumAsync(),
                CurrentPage = page
            };

            if (!string.IsNullOrEmpty(searchText))
            {
                ViewData["Title"] = $"Search Results For {searchText}";
            }

            else
            {
                ViewData["Title"] = "Delete only abusive or inappropriate comments!";
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id, string zone, int page)
        {
            var commentExists = await this.comments.ExistsAsync(id);
            if (!zone.IsValidCommentZone() || !commentExists || page < 1)
            {
                return BadRequest();
            }

           await this.comments.DeleteAsync(id);

           return zone.ToLower() == "news"
                ? RedirectToAction(nameof(NewsComments))
                : RedirectToAction(nameof(ForumComments));
        }
        #endregion
    }
}
