using System;

namespace Application.Response
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public int UserAccountId { get; set; }
        public string AuthorName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
} 