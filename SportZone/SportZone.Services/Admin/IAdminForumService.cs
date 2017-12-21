using SportZone.Services.Admin.Models.Forum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportZone.Services.Admin
{
    public interface IAdminForumService
    {
        Task<IEnumerable<AdminArticlesListingServiceModel>> AllAsync(string searchText = null, int page = 1);

        Task<int> TotalAsync(string searchText);

        Task DeleteAsync(int id);
    }
}