using AutoMapper;
using SportZone.Common.Mapping;
using SportZone.Data.Models;
using System;

namespace SportZone.Services.Newz.Models
{
    public class NewsCommentsServiceModel : IMapFrom<Comment>, IHaveCustomMapping
    {
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
                => mapper
                   .CreateMap<Comment, NewsCommentsServiceModel>()
                   .ForMember(n => n.Author, cfg => cfg.MapFrom(n => n.Author.UserName));
    }
}