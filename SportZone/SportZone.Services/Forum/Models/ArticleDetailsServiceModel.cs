using AutoMapper;
using SportZone.Common.Mapping;
using SportZone.Data.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SportZone.Services.Forum.Models
{
    public class ArticleDetailsServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public string Author { get; set; }

        public List<Comment> Comments { get; set; }

        public int CommentsCount { get; set; }

        public void ConfigureMapping(Profile mapper)
                => mapper
                    .CreateMap<Article, ArticleDetailsServiceModel>()
                    .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName))
                    .ForMember(a => a.CommentsCount, cfg => cfg.MapFrom(a => a.Comments.Count));
    }
}