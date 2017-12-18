using AutoMapper;
using SportZone.Common.Mapping;
using SportZone.Data.Models;
using System;

namespace SportZone.Services.Models
{
    public class CommentsServiceModel : IMapFrom<Comment>, IHaveCustomMapping
    {
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
                => mapper
                   .CreateMap<Comment, CommentsServiceModel>()
                   .ForMember(n => n.Author, cfg => cfg.MapFrom(n => n.Author.UserName));
    }
}
