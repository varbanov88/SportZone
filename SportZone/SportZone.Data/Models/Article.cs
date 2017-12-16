using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Data.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ContentMinLength)]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? LastCommentDate { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
