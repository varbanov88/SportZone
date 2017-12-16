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

        public async Task CreateAsync(string userId, IFormFile image, string title, string content, string videoUrl, HashSet<string> tags)
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

            var existingTags = this.db.Tag
                .Where(t => tags.Contains(t.Content))
                .ToList();

            var newTags = new List<string>();
            foreach (var tag in tags)
            {
                if (!existingTags.Any(t => t.Content == tag))
                {
                    newTags.Add(tag);
                }
            }

            foreach (var tag in existingTags)
            {
                tag.NewsTagged.Add(new NewsTag
                {
                    NewsId = news.Id
                });
            }

            foreach (var tag in newTags)
            {
                var newTag = new Tag { Content = tag };
                newTag.NewsTagged.Add(new NewsTag
                {
                    NewsId = news.Id
                });

                this.db.Tag.Add(newTag);
            }

            await this.db.SaveChangesAsync();
        }

        public async Task<NewsDetailsServiceModel> GetByIdAsync(int id)
        { 
            var news = await this.db
                .News
                .Where(a => a.Id == id)
                .ProjectTo<NewsDetailsServiceModel>()
                .FirstOrDefaultAsync();

            foreach (var tag in news.Tags)
            {
                var dbTag = db.Tag.Where(t => t.Id == tag.TagId).FirstOrDefault();
                tag.Tag.Content = dbTag.Content;
            }

            return news;
        }

        public async Task AddCommentAsync(int articleId, string comment, string userId)
        {
            var article = await this.db
                .News
                .FindAsync(articleId);

            var currentTime = DateTime.UtcNow;

            var articleComment = new Comment
            {
                NewsId = articleId,
                AuthorId = userId,
                Content = comment,
                PublishDate = currentTime,
                IsForNews = true
            };

            article.Comments.Add(articleComment);

            await db.SaveChangesAsync();
        }

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