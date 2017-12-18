using SportZone.Services.Newz.Models;
using System;
using System.Collections.Generic;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.News.Models
{
    public class CommentViewModel
    {
        public int NewsId { get; set; }

        public string NewsTitle { get; set; }

        public IEnumerable<NewsCommentsServiceModel> Comments { get; set; } = new List<NewsCommentsServiceModel>();

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