using SportZone.Services.Admin.Models.Comments;
using System.Collections.Generic;

namespace SportZone.Web.Areas.Admin.Models.Comments
{
    public class NewsCommentsListingViewModel : CommentListingBaseModel
    {
        public IEnumerable<NewsCommentsListingModel> Comments { get; set; } = new List<NewsCommentsListingModel>();

        public int TotalForumComments { get; set; }
    }
}