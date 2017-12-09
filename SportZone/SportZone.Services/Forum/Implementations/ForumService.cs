﻿using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Data.Models;
using SportZone.Services.Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Services.Forum.Implementations
{
    public class ForumService : IForumService
    {
        private readonly SportZoneDbContext db;

        public ForumService(SportZoneDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ArticleListingServiceModel>> AllArticlesAsync(string searchText = null, int page = 1)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                    .Articles
                    .OrderByDescending(a => a.DateCreated)
                    .ThenByDescending(a => a.LastCommentDate)
                    .ThenByDescending(a => a.Comments.Count)
                    .Where(a => a.Title.ToLower().Contains(searchText.ToLower()))
                    .Skip((page - 1) * ArticleListingPageSize)
                    .Take(ArticleListingPageSize)
                    .ProjectTo<ArticleListingServiceModel>()
                    .ToListAsync();
        }

        public async Task<int> TotalAsync(string searchText)
        {
            var articles = string.IsNullOrEmpty(searchText)
                ? await this.db.Articles.CountAsync()
                : await this.db.Articles
                            .Where(a => a.Title.ToLower().Contains(searchText.ToLower()))
                            .CountAsync();
            return articles;
        }

        public async Task CreateAsync(string title, string content, string authorId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                DateCreated = DateTime.UtcNow,
                AuthorId = authorId
            };

            this.db.Add(article);

            await this.db.SaveChangesAsync();
        }

        public async Task<ArticleDetailsServiceModel> GetByIdAsync(int id)
            => await this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task AddCommentAsync(int articleId, string comment, string userId)
        {
            var article = await this.db
                .Articles
                .FindAsync(articleId);

            var currentTime = DateTime.UtcNow;

            var articleComment = new ForumComment
            {
                ArticleId = articleId,
                AuthorId = userId,
                Content = comment,
                PublishDate = currentTime
            };

            article.Comments.Add(articleComment);
            article.LastCommentDate = currentTime;

            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ArticleListingServiceModel>> FindAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Articles
                .OrderByDescending(a => a.Id)
                .ThenByDescending(a => a.LastCommentDate)
                .Where(a => a.Title.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<ArticleListingServiceModel>()
                .ToListAsync();
        }
    }
}