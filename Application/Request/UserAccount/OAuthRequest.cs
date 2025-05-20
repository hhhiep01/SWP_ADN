using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.UserAccount
{
    public class OAuthRequest
    {
        [Required]
        public string Provider { get; set; }
        
        [Required]
        public string Token { get; set; }
    }

    public class GoogleAuthRequest
    {
        [Required]
        public string Token { get; set; }
    }

    public class FacebookAuthRequest
    {
        [Required]
        public string Token { get; set; }
    }
} 