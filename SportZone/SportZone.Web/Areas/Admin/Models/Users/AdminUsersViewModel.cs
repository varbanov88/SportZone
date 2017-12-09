using Microsoft.AspNetCore.Mvc.Rendering;
using SportZone.Services.Admin.Models;
using System.Collections.Generic;

namespace SportZone.Web.Areas.Admin.Models.Users
{
    public class AdminUsersViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
