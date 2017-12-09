using System.Collections.Generic;

namespace SportZone.Web.Areas.Forum.Models
{
    public class SearchForumModel
    {
        public string SearchText { get; set; }

        public IEnumerable<ArticleListingModel> Articles { get; set; }
    }
}