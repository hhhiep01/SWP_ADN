using System.Collections.Generic;

namespace Domain.Entity
{
    public class Role : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserAccount> UserAccounts { get; set; }
    }
} 