using SportZone.Services.Admin.Models.Comments;
using System.Collections.Generic;

namespace SportZone.Web.Areas.Admin.Models.Comments
{
    public class ForumCommentsListingViewModel : CommentListingBaseModel
    {
        public IEnumerable<ForumCommentsListingModel> Comments { get; set; } = new List<ForumCommentsListingModel>();

        public int TotalNewsComments { get; set; }
    }
}