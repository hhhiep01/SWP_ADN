using System.ComponentModel.DataAnnotations;

namespace Application.Request.Blog
{
    public class UpdateBlogRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Content { get; set; }
    }
} 