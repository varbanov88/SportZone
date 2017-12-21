using SportZone.Services.Admin.Models.News;
using System;
using System.Collections.Generic;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.Admin.Models.News
{
    public class NewsListingViewModel
    {
        public IEnumerable<AdminNewsListingServiceModel> News { get; set; }

        public int TotalNews { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalNews / AdminPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}