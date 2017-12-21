using AutoMapper;
using SportZone.Common.Mapping;
using SportZone.Data.Models;
using System;

namespace SportZone.Services.Admin.Models.Forum
{
    public class AdminArticlesListingServiceModel : IMapFrom<Article>, IHaveCustomMapping
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
                   .CreateMap<Article, AdminArticlesListingServiceModel>()
                   .ForMember(n => n.Author, cfg => cfg.MapFrom(n => n.Author.UserName))
                   .ForMember(n => n.Comments, cfg => cfg.MapFrom(n => n.Comments.Count));
    }
}