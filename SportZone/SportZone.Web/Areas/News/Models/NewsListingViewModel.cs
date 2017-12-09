using SportZone.Services.Newz.Models;
using System.Collections.Generic;

namespace SportZone.Web.Areas.News.Models
{
    public class NewsListingViewModel
    {
        public IEnumerable<NewsListingServiceModel> Articles { get; set; } = new List<NewsListingServiceModel>();
    }
}
