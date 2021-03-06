﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        public List<Article> Articles { get; set; } = new List<Article>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<News> News { get; set; } = new List<News>();
    }
}
