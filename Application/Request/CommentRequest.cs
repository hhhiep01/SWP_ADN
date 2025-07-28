using System.ComponentModel.DataAnnotations;

namespace Application.Request
{
    public class CommentRequest
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int BlogId { get; set; }
    }
} 