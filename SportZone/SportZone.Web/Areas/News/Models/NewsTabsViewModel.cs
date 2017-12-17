using SportZone.Services.Newz.Models;
using System.Collections.Generic;

namespace SportZone.Web.Areas.News.Models
{
    public class NewsTabsViewModel
    {
        public IEnumerable<TabsNewsServiceModel> LatestNews { get; set; } = new List<TabsNewsServiceModel>();

        public IEnumerable<TabsNewsServiceModel> MostReadNews { get; set; } = new List<TabsNewsServiceModel>();
    }
}