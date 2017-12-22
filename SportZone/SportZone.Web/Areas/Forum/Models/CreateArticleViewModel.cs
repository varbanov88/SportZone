using System.ComponentModel.DataAnnotations;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Areas.Forum.Models
{
    public class CreateArticleViewModel
    {
        [Required]
        [MinLength(TitleMinLength, ErrorMessage = "Title must be at least 5 symbols.")]
        [MaxLength(TitleMaxLength, ErrorMessage = "Title must not be more than 100 symbols.")]
        public string Title { get; set; }

        [Required]
        [MinLength(ContentMinLength, ErrorMessage = "Content must be at least 50 symbols.")]
        [MaxLength(ContentMaxLength, ErrorMessage = "Content must not be more than 2000 symbols.")]
        public string Content { get; set; }
    }
}
