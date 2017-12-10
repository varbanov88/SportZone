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

namespace SportZone.Services.Newz.Implementations
{
    public class NewsService : BasicService, INewsService
    {
        public NewsService(SportZoneDbContext db) : base(db) {}

        public async Task<IEnumerable<NewsListingServiceModel>> AllAsync()
        {
            var news = await this.db
                            .News
                            .ProjectTo<NewsListingServiceModel>()
                            .ToListAsync();
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
    }
}