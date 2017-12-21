using SportZone.Services.Admin.Models.News;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportZone.Services.Admin
{
    public interface IAdminNewsService
    {
        Task<IEnumerable<AdminNewsListingServiceModel>> AllAsync(string searchText = null, int page = 1);

        Task<int> TotalAsync(string searchText);

    }
}