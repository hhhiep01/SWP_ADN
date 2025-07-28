using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class Blog : Base
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public int UserAccountId { get; set; }

        // Navigation property
        public UserAccount UserAccount { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
} 