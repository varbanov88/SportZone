﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Data.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ContentMinLength)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsEdited { get; set; }

        public DateTime? LastEditedDate { get; set; }

        [MinLength(VideoUrlLength)]
        [MaxLength(VideoUrlLength)]
        public string VideoUrl { get; set; }

        [MinLength(ImageMinSize)]
        [MaxLength(ImageMaxSize)]
        public byte[] Image { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public List<NewsComment> Comments { get; set; }

        public IEnumerable<NewsTag> Tags { get; set; } = new List<NewsTag>();
    }
}