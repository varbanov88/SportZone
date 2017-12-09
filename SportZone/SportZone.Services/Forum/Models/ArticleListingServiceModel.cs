using AutoMapper;
using SportZone.Common.Mapping;
using SportZone.Data.Models;
using System;

namespace SportZone.Services.Forum.Models
{
    public class ArticleListingServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? LastCommentDate { get; set; }

        public string Author { get; set; }

        public int Comments { get; set; }

        public void ConfigureMapping(Profile mapper)
                => mapper
                    .CreateMap<Article, ArticleListingServiceModel>()
                    .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName))
                    .ForMember(a => a.Comments, cfg => cfg.MapFrom(a => a.Comments.Count));
    }
}
