using Microsoft.AspNetCore.Http;
using SportZone.Services.Newz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportZone.Services.Newz
{
    public interface INewsService
    {
        Task<IEnumerable<NewsListingServiceModel>> AllAsync(string searchText = null, int page = 1);

        Task<int> TotalAsync(string searchText);

        Task CreateAsync(string userId, IFormFile image, string title, string content, string videoUrl, HashSet<string> tags);

        Task<NewsDetailsServiceModel> GetByIdAsync(int id);

        Task DeleteAsync(int id);

        Task AddCommentAsync(int articleId, string comment, string userId);
    }
}
