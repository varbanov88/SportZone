using System;

namespace SportZone.Services.Admin.Models.Comments
{
    public abstract class BaseCommentsListingModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime PublishDate { get; set; }
    }
}