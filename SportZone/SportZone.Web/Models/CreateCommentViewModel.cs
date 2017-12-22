using System.ComponentModel.DataAnnotations;

namespace SportZone.Web.Models
{
    public class CreateCommentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string Comment { get; set; }
    }
}