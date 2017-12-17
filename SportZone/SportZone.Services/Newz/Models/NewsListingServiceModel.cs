using SportZone.Common.Mapping;
using SportZone.Data.Models;
using System;

namespace SportZone.Services.Newz.Models
{
    public class NewsListingServiceModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime? LastEditedDate { get; set; }

        public string AuthorId { get; set; }

        public byte[] Image { get; set; }

        public int ReadCount { get; set; }       
    }
}