using Microsoft.AspNetCore.Http;
using SportZone.Services.Newz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportZone.Services.Newz
{
    public interface INewsService
    {
        Task<IEnumerable<NewsListingServiceModel>> AllAsync();

        Task CreateAsync(string userId, IFormFile image, string title, string content, string videoUrl);
    }
}
