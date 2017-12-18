using SportZone.Services.Models;
using System;
using System.Collections.Generic;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.Forum.Models
{
    public class CommentViewModel
    {
        public int ArticleId { get; set; }

        public string ArticleTitle { get; set; }

        public string Content { get; set; }

        public IEnumerable<CommentsServiceModel> Comments { get; set; } = new List<CommentsServiceModel>();

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