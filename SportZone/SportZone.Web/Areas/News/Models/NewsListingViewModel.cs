using SportZone.Services.Newz.Models;
using System;
using System.Collections.Generic;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.News.Models
{
    public class NewsListingViewModel
    {
        public IEnumerable<NewsListingServiceModel> Articles { get; set; } = new List<NewsListingServiceModel>();

        public int TotalNews { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalNews / NewsPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
