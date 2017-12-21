using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Services.Admin.Models.Comments;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Services.Admin.Implementations
{
    public class AdminCommentService : BasicService, IAdminCommentService
    {
        public AdminCommentService(SportZoneDbContext db) : base(db) { }

        public async Task<bool> ExistsAsync(int id)
        {
            var comment = await this.db.Comments.FirstOrDefaultAsync(c => c.Id == id);

            return comment != null;
        }

        public async Task<IEnumerable<ForumCommentsListingModel>> AllForumAsync(string searchText = null, int page = 1)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                    .Comments
                    .OrderByDescending(c => c.PublishDate)
                    .Where(c => c.IsForArticle == true && c.Content.ToLower().Contains(searchText.ToLower()))
                    .Skip((page - 1) * AdminPageSize)
                    .Take(AdminPageSize)
                    .ProjectTo<ForumCommentsListingModel>()
                    .ToListAsync();
        }

        public async Task<int> TotalForumAsync()
             => await this.db
                          .Comments
                          .Where(c => c.IsForArticle == true)
                          .CountAsync();
               

        public async Task<IEnumerable<NewsCommentsListingModel>> AllNewsAsync(string searchText = null, int page = 1)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                    .Comments
                    .OrderByDescending(c => c.PublishDate)
                    .Where(c => c.IsForNews == true && c.Content.ToLower().Contains(searchText.ToLower()))
                    .Skip((page - 1) * AdminPageSize)
                    .Take(AdminPageSize)
                    .ProjectTo<NewsCommentsListingModel>()
                    .ToListAsync();
        }

        public async Task<int> TotalNewsAsync()
            => await this.db
                         .Comments
                         .Where(c => c.IsForNews == true)
                         .CountAsync();

        public async Task DeleteAsync(int id)
        {
            this.db
                .Comments
                .Remove(await this.db.Comments.FirstOrDefaultAsync(c => c.Id == id));

            await this.db.SaveChangesAsync();
        }
    }
}