using SportZone.Services.Admin.Models.Comments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportZone.Services.Admin
{
    public interface IAdminCommentService
    {
        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<ForumCommentsListingModel>> AllForumAsync(string searchText = null, int page = 1);

        Task<int> TotalForumAsync();

        Task<IEnumerable<NewsCommentsListingModel>> AllNewsAsync(string searchText = null, int page = 1);

        Task<int> TotalNewsAsync();

        Task DeleteAsync(int id);
    }
}