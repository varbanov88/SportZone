using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.News.Models
{
    public class CreateNewsViewModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ContentMinLength)]
        public string Content { get; set; }

        [MinLength(VideoUrlLength)]
        [MaxLength(VideoUrlLength)]
        public string VideoUrl { get; set; }

        public IFormFile Image { get; set; }
    }
}
