using SportZone.Common.Mapping;
using SportZone.Data.Models;

namespace SportZone.Services.Newz.Models
{
    public class TabsNewsServiceModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}