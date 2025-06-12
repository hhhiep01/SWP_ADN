using System.ComponentModel.DataAnnotations;

namespace Application.Request.Blog
{
    public class CreateBlogRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Content { get; set; }
    }
} 