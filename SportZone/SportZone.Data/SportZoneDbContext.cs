﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportZone.Data.Models;

namespace SportZone.Data
{
    public class SportZoneDbContext : IdentityDbContext<User>
    {
        public SportZoneDbContext(DbContextOptions<SportZoneDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ForumComment> ForumComments { get; set; }

        public DbSet<NewsComment> NewsCommnets { get; set; }

        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Article>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Article)
                .HasForeignKey(c => c.ArticleId);

            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder.Entity<User>()
                .HasMany(u => u.Articles)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AuthorId);

            builder.Entity<User>()
                .HasMany(u => u.NewsComments)
                .WithOne(nc => nc.Author)
                .HasForeignKey(nc => nc.AuthorId);

            builder.Entity<User>()
                .HasMany(u => u.ForumComments)
                .WithOne(fc => fc.Author)
                .HasForeignKey(fc => fc.AuthorId);

            builder.Entity<User>()
                .HasMany(u => u.News)
                .WithOne(n => n.Author)
                .HasForeignKey(n => n.AuthorId);

            builder.Entity<News>()
                .HasMany(n => n.Comments)
                .WithOne(c => c.News)
                .HasForeignKey(c => c.NewsId);

            base.OnModelCreating(builder);
        }
    }
}
