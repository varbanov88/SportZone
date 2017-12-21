using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Services.Admin.Models.News;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Services.Admin.Implementations
{
    public class AdminNewsService : BasicService, IAdminNewsService
    {
        public AdminNewsService(SportZoneDbContext db) : base(db) { }

        public async Task<IEnumerable<AdminNewsListingServiceModel>> AllAsync(string searchText = null, int page = 1)
            => await this.db
                    .News
                    .Where(n => n.Title.ToLower().Contains(searchText))
                    .OrderByDescending(n => n.PublishDate)
                    .ThenByDescending(n => n.LastEditedDate)
                    .Skip((page - 1) * AdminPageSize)
                    .Take(AdminPageSize)
                    .ProjectTo<AdminNewsListingServiceModel>()
                    .ToListAsync();

        public async Task<int> TotalAsync(string searchText)
        {
            var totalNews = string.IsNullOrEmpty(searchText)
                ? await this.db.News.CountAsync()
                : await this.db.News
                            .Where(n => n.Title.ToLower().Contains(searchText))
                            .CountAsync();
            return totalNews;
        }
    }
}
