using SportZone.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportZone.Services.Admin
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync(string searchText = null, int page = 1);

        Task<int> TotalAsync(string searchText);
    }
}