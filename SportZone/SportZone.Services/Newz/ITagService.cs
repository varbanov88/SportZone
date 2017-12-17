using SportZone.Services.Newz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportZone.Services.Newz
{
    public interface ITagService
    {
        IEnumerable<NewsListingServiceModel> All(int tagId, int page = 1);

        string GetName(int id);

        int TotalNews(int tagId);

        Task<IEnumerable<PopularTagServiceModel>> GetPopularAsync();
    }
}
