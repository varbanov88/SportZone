using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Services.Newz.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Services.Newz.Implementations
{
    public class TagService : BasicService, ITagService
    {
        public TagService(SportZoneDbContext db) : base(db) { }

        public IEnumerable<NewsListingServiceModel> All(int tagId, int page = 1)
        {
            var newsIds = this.db
                .NewsTag
                .Where(n => n.TagId == tagId)
                .Select(n => n.NewsId)
                .ToList();

            return this.db
                .News
                .OrderByDescending(n => n.PublishDate)
                .OrderByDescending(n => n.LastEditedDate)
                .Where(n => newsIds.Contains(n.Id))
                .Skip((page - 1) * NewsPageSize)
                .Take(NewsPageSize)
                .ProjectTo<NewsListingServiceModel>()
                .ToList();
        }

        public string GetName(int id)
                => this.db
                       .Tag
                       .Where(t => t.Id == id)
                       .Select(t => t.Content)
                       .FirstOrDefault();

        public int TotalNews(int tagId)
            =>  this.db
                .NewsTag
                .Where(n => n.TagId == tagId)
                .Select(n => n.NewsId)
                .Count();

        public async Task<IEnumerable<PopularTagServiceModel>> GetPopularAsync()
        {
            var tagsGroups = await this.db
                    .NewsTag
                    .GroupBy(t => t.TagId)
                    .Select(group => new
                        {
                            TagId = group.Key,
                            Count = group.Count()
                        })
                    .OrderByDescending(t => t.Count)
                    .Take(8)
                    .ToListAsync();

            var tagsIds = tagsGroups
                .Select(tg => tg.TagId)
                .ToList();

            return await this.db
                    .Tag
                    .Where(t => tagsIds.Contains(t.Id))
                    .ProjectTo<PopularTagServiceModel>()
                    .ToListAsync();
        }
    }
}
