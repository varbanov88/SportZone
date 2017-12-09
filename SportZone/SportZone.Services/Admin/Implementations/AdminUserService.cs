using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportZone.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly SportZoneDbContext db;

        public AdminUserService(SportZoneDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
            => await this.db
                    .Users
                    .ProjectTo<AdminUserListingServiceModel>()
                    .ToListAsync();
    }
}
