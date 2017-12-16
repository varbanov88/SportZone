using AutoMapper.QueryableExtensions;
using SportZone.Data;
using SportZone.Services.Newz.Models;
using System.Collections.Generic;
using System.Linq;

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
    }
}
