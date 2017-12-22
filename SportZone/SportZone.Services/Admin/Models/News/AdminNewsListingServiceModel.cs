using AutoMapper;
using SportZone.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportZone.Services.Admin.Models.News
{
    public class AdminNewsListingServiceModel : IMapFrom<Data.Models.News>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishDate { get; set; }

        public int ReadCount { get; set; }

        public string Author { get; set; }

        public int Comments { get; set; }

        public List<string> Tags { get; set; }
       
        public void ConfigureMapping(Profile mapper)
              => mapper
                   .CreateMap<Data.Models.News, AdminNewsListingServiceModel>()
                   .ForMember(n => n.Author, cfg => cfg.MapFrom(n => n.Author.UserName))
                   .ForMember(n => n.Comments, cfg => cfg.MapFrom(n => n.Comments.Count))
                   .ForMember(n => n.Tags, cfg => cfg.MapFrom(n => n.Tags
                                                                    .Select(t => t.Tag.Content)
                                                                    .ToList()));
    }
}