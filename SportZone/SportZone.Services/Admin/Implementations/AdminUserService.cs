using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Services.Admin.Implementations
{
    public class AdminUserService : BasicService, IAdminUserService
    {
        public AdminUserService(SportZoneDbContext db) : base(db){}

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync(string searchText = null, int page = 1)
            => await this.db
                    .Users
                    .Where(u => u.UserName.ToLower().Contains(searchText))
                    .OrderBy(u => u.Id)
                    .Skip((page - 1) * AdminPageSize)
                    .Take(AdminPageSize)
                    .ProjectTo<AdminUserListingServiceModel>()
                    .ToListAsync();

        public async Task<int> TotalAsync(string searchText)
        {
            var totalUsers = string.IsNullOrEmpty(searchText)
                ? await this.db.Users.CountAsync()
                : await this.db.Users
                            .Where(u => u.UserName.ToLower().Contains(searchText))
                            .CountAsync();
            return totalUsers;
        }
    }
}