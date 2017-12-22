using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Services.Admin.Models.Forum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Services.Admin.Implementations
{
    public class AdminForumService : BasicService, IAdminForumService
    {
        public AdminForumService(SportZoneDbContext db) : base(db) { }

        public async Task<IEnumerable<AdminArticlesListingServiceModel>> AllAsync(string searchText = null, int page = 1)
             => await this.db
                        .Articles
                        .Where(n => n.Title.ToLower().Contains(searchText))
                        .OrderByDescending(n => n.DateCreated)
                        .Skip((page - 1) * AdminPageSize)
                        .Take(AdminPageSize)
                        .ProjectTo<AdminArticlesListingServiceModel>()
                        .ToListAsync();

        public async Task<int> TotalAsync(string searchText)
             => string.IsNullOrEmpty(searchText)
                    ? await this.db.Articles.CountAsync()
                    : await this.db.Articles
                                .Where(n => n.Title.ToLower().Contains(searchText))
                                .CountAsync();

        public async Task DeleteAsync(int id)
        {
            var comments = await this.db
                               .Comments
                               .Where(c => c.IsForArticle == true && c.ArticleId == id)
                               .ToListAsync();

            this.db.Comments.RemoveRange(comments);
            this.db.Articles.Remove(await this.db
                .Articles
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync());
            await this.db.SaveChangesAsync();
        }

    }
}