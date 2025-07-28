using System;

namespace Domain.Entity
{
    public class Comment : Base
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public int UserAccountId { get; set; }

        // Navigation properties
        public Blog Blog { get; set; }
        public UserAccount UserAccount { get; set; }
    }
} 