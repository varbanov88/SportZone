using AutoMapper;
using SportZone.Common.Mapping;
using SportZone.Data.Models;
using System;

namespace SportZone.Services.Newz.Models
{
    public class NewsListingServiceModel : IMapFrom<News>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime? LastEditedDate { get; set; }

        public byte[] Image { get; set; }

        public int Comments { get; set; }

        public void ConfigureMapping(Profile mapper)
               => mapper
                    .CreateMap<News, NewsListingServiceModel>()
                    .ForMember(a => a.Comments, cfg => cfg.MapFrom(a => a.Comments.Count));
    }
}