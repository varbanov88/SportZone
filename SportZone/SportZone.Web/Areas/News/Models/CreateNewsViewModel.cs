using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.News.Models
{
    public class CreateNewsViewModel
    {
        [Required]
        [MinLength(TitleMinLength, ErrorMessage = "Title must be at least 5 symbols.")]
        [MaxLength(TitleMaxLength, ErrorMessage = "Title must not be more than 100 symbols.")]
        public string Title { get; set; }

        [Required]
        [MinLength(ContentMinLength, ErrorMessage = "Title must be at least 50 symbols.")]
        public string Content { get; set; }

        [MinLength(VideoUrlLength, ErrorMessage = "VideoUrl must be 11 symbols.")]
        [MaxLength(VideoUrlLength)]
        public string VideoUrl { get; set; }

        public IFormFile Image { get; set; }
        
        [Required]
        public string Tags { get; set; }
    }
}
