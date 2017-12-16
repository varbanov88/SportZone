using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public DbSet<News> News { get; set; }

        public DbSet<Tag> Tag { get; set; }

        public DbSet<NewsTag> NewsTag { get; set; }

        public DbSet<Comment> Comments { get; set; }

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
                .HasMany(u => u.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(nc => nc.AuthorId);

            builder.Entity<User>()
                .HasMany(u => u.News)
                .WithOne(n => n.Author)
                .HasForeignKey(n => n.AuthorId);

            builder.Entity<News>()
                .HasMany(n => n.Comments)
                .WithOne(c => c.News)
                .HasForeignKey(c => c.NewsId);

            builder.Entity<News>()
                .HasMany(n => n.Tags)
                .WithOne(t => t.News)
                .HasForeignKey(t => t.NewsId);

            builder.Entity<NewsTag>()
                .HasKey(nt => new { nt.TagId, nt.NewsId });

            builder.Entity<Tag>()
                .HasMany(t => t.NewsTagged)
                .WithOne(n => n.Tag)
                .HasForeignKey(n => n.TagId);

            base.OnModelCreating(builder);
        }
    }
}
