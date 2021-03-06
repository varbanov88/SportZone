﻿using System.ComponentModel.DataAnnotations;

namespace SportZone.Web.Areas.Admin.Models.Users
{
    public class RoleFormModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}