using AutoMapper;
using SportZone.Common.Mapping;
using SportZone.Data.Models;

namespace SportZone.Services.Admin.Models.Comments
{
    public class ForumCommentsListingModel : BaseCommentsListingModel, IMapFrom<Comment>, IHaveCustomMapping
    {
        public string ArticleTitle { get; set; }

        public void ConfigureMapping(Profile mapper)
              => mapper
                   .CreateMap<Comment, ForumCommentsListingModel>()
                   .ForMember(fc => fc.Author, cfg => cfg.MapFrom(c => c.Author.UserName))
                   .ForMember(fc => fc.ArticleTitle, cfg => cfg.MapFrom(c => c.Article.Title));
    }
}