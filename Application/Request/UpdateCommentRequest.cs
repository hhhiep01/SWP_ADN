using System.ComponentModel.DataAnnotations;

namespace Application.Request
{
    public class UpdateCommentRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int BlogId { get; set; }
    }
} 