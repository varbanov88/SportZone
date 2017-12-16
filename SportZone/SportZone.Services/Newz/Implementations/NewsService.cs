using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Data.Models;
using SportZone.Services.Newz.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Services.Newz.Implementations
{
    public class NewsService : BasicService, INewsService
    {
        public NewsService(SportZoneDbContext db) : base(db) {}

        public async Task<IEnumerable<NewsListingServiceModel>> AllAsync(string searchText = null, int page = 1)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                    .News
                    .OrderByDescending(n => n.PublishDate)
                    .OrderByDescending(n => n.LastEditedDate)
                    .Where(n => n.Title.ToLower().Contains(searchText.ToLower()))
                    .Skip((page - 1) * NewsPageSize)
                    .Take(NewsPageSize)
                    .ProjectTo<NewsListingServiceModel>()
                    .ToListAsync();
        }

        public async Task<int> TotalAsync(string searchText)
        {
            var news = string.IsNullOrEmpty(searchText)
                ? await this.db.News.CountAsync()
                : await this.db.News
                            .Where(n => n.Title.ToLower().Contains(searchText.ToLower()))
                            .CountAsync();
            return news;
        }

        public async Task CreateAsync(string userId, IFormFile image, string title, string content, string videoUrl)
        {
            var news = new News
            {
                AuthorId = userId,
                Content = content,
                Title = title,
                PublishDate = DateTime.UtcNow,
                VideoUrl = videoUrl
            };

            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                news.Image = memoryStream.ToArray();
            }

            await this.db.AddAsync(news);
            await this.db.SaveChangesAsync();
        }

        public async Task<NewsDetailsServiceModel> GetByIdAsync(int id)
            => await this.db
                .News
                .Where(a => a.Id == id)
                .ProjectTo<NewsDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task DeleteAsync(int id)
        {
            var news = await this.db
                 .News
                 .Where(n => n.Id == id)
                 .FirstOrDefaultAsync();

            this.db.News.Remove(news);
            await this.db.SaveChangesAsync();
        }
    }
}