using AutoMapper;
using SportZone.Common.Mapping;
using SportZone.Data.Models;
using System;
using System.Collections.Generic;

namespace SportZone.Services.Newz.Models
{
    public class NewsDetailsServiceModel : IMapFrom<News>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsEdited { get; set; }

        public DateTime? LastEditedDate { get; set; }

        public string VideoUrl { get; set; }

        public byte[] Image { get; set; }

        public string Author { get; set; }

        public List<NewsComment> Comments { get; set; }

        public void ConfigureMapping(Profile mapper)
              => mapper
                   .CreateMap<News, NewsDetailsServiceModel>()
                   .ForMember(n => n.Author, cfg => cfg.MapFrom(n => n.Author.UserName));
    }
}
