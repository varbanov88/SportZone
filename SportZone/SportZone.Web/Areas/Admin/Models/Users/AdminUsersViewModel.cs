using Microsoft.AspNetCore.Mvc.Rendering;
using SportZone.Services.Admin.Models;
using System;
using System.Collections.Generic;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.Admin.Models.Users
{
    public class AdminUsersViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

        public IDictionary<string, IList<string>> UserRoles { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

        public int TotalUsers { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalUsers / AdminPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}