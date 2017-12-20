using System;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.Admin.Models.Comments
{
    public class CommentListingBaseModel
    {
        public int TotalComments { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalComments / CommentPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}