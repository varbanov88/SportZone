using AutoMapper;
using SportZone.Common.Mapping;
using SportZone.Data.Models;

namespace SportZone.Services.Admin.Models.Comments
{
    public class NewsCommentsListingModel : BaseCommentsListingModel, IMapFrom<Comment>, IHaveCustomMapping
    {
        public string NewsTitle { get; set; }

        public void ConfigureMapping(Profile mapper)
              => mapper
                   .CreateMap<Comment, NewsCommentsListingModel>()
                   .ForMember(nc => nc.Author, cfg => cfg.MapFrom(c => c.Author.UserName))
                   .ForMember(nc => nc.NewsTitle, cfg => cfg.MapFrom(c => c.News.Title));
    }
}