using SportZone.Common.Mapping;
using SportZone.Data.Models;

namespace SportZone.Services.Newz.Models
{
    public class PopularTagServiceModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
