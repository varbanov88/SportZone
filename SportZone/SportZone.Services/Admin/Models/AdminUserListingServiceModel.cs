using SportZone.Common.Mapping;
using SportZone.Data.Models;

namespace SportZone.Services.Admin.Models
{
    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
