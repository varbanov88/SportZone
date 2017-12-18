using SportZone.Services.Forum.Models;
using SportZone.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportZone.Services.Forum
{
    public interface IForumService
    {
        Task<IEnumerable<ArticleListingServiceModel>> AllArticlesAsync(string searchText = null, int page = 1);

        Task<int> TotalAsync(string searchText);

        Task<int> TotalCommentsAsync(int id);

        Task CreateAsync(string title, string content, string authorId);

        Task<ArticleDetailsServiceModel> GetByIdAsync(int id);

        Task<IEnumerable<CommentsServiceModel>> GetCommentsAsync(int id, int page = 1);

        Task AddCommentAsync(int articleId, string comment, string userId);

        Task<IEnumerable<ArticleListingServiceModel>> FindAsync(string searchText);
    }
}
