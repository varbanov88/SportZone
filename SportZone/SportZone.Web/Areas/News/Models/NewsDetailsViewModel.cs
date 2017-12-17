using SportZone.Services.Newz.Models;
using System.Collections.Generic;

namespace SportZone.Web.Areas.News.Models
{
    public class NewsDetailsViewModel
    {
        public NewsDetailsServiceModel News { get; set; }

        public NewsTabsViewModel NewsTabs { get; set; } = new NewsTabsViewModel();

        public IEnumerable<PopularTagServiceModel> Tags { get; set; }
    }
}