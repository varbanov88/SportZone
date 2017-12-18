using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Data.Models;
using SportZone.Services.Models;
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
                    .ThenByDescending(n => n.LastEditedDate)
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

        public async Task<int> TotalCommentsAsync(int id)
            => await this.db
                .Comments
                .Where(c => c.NewsId == id)
                .CountAsync();

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

            if (image != null)
            {
                news.Image = await ProcessImage(image);
            }

            await this.db.AddAsync(news);
            await this.db.SaveChangesAsync();

            var existingTags = await this.db.Tag
                .Where(t => tags.Contains(t.Content))
                .ToListAsync();

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


        public async Task EditAsync(int id, IFormFile image, string title, string content, string videoUrl, HashSet<string> tags)
        {
            var news = await this.db.News.Where(n => n.Id == id).FirstOrDefaultAsync();
            news.Title = title;
            news.Content = content;
            news.VideoUrl = videoUrl;
            news.IsEdited = true;
            news.LastEditedDate = DateTime.UtcNow;

            if (image != null)
            {
                news.Image = await ProcessImage(image);
            }

            var existingTagsIds = news.Tags.Select(t => t.TagId);
            var existingTags = await this.db
                .Tag
                .Where(t => existingTagsIds.Contains(t.Id))
                .Select(t => t.Content)
                .ToListAsync();

            foreach (var tag in tags)
            {
                if (!existingTags.Contains(tag))
                {
                    var newTag = new Tag { Content = tag };
                    newTag.NewsTagged.Add(new NewsTag
                    {
                        NewsId = id
                    });

                    this.db.Tag.Add(newTag);
                }
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

        public async Task<IEnumerable<CommentsServiceModel>> GetCommentsAsync(int id, int page = 1)
          => await this.db
                .Comments
                .Where(c => c.NewsId == id)
                .OrderBy(c => c.PublishDate)
                .Skip((page - 1) * CommentPageSize)
                .Take(CommentPageSize)
                .ProjectTo<CommentsServiceModel>()
                .ToListAsync();

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

        private async Task<byte[]> ProcessImage(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async Task ReadAsync(int id)
        {
            var news = await this.db
                .News
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();
            news.ReadCount++;
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TabsNewsServiceModel>> LatestAsync()
            => await this.db
                .News
                .OrderByDescending(n => n.PublishDate)
                .ThenByDescending(n => n.LastEditedDate)
                .Take(5)
                .ProjectTo<TabsNewsServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<TabsNewsServiceModel>> MostReadAsync()
            => await this.db
                .News
                .Where(n => (DateTime.UtcNow - n.PublishDate).TotalDays < 7)
                .OrderByDescending(n => n.ReadCount)
                .Take(5)
                .ProjectTo<TabsNewsServiceModel>()
                .ToListAsync();
    }
}