using SportZone.Services.Admin.Models.Forum;
using System;
using System.Collections.Generic;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.Admin.Models.Forum
{
    public class ArticleListingViewModel
    {
        public IEnumerable<AdminArticlesListingServiceModel> Articles { get; set; }

        public int TotalArticles { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalArticles / AdminPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}