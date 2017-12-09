using System;
using System.ComponentModel.DataAnnotations;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Data.Models
{
    public class NewsComment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(CommentMinLength)]
        [MaxLength(CommentMaxLength)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public int NewsId { get; set; }

        public News News { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
