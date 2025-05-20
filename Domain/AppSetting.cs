using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AppSetting
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public SecretToken SecretToken { get; set; }
        public Authentication Authentication { get; set; }
        public OAuth OAuth { get; set; }
    }
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
        public string LocalDockerConnection { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class SecretToken
    {
        public string Value { get; set; }
    }

    public class Authentication
    {
        public GoogleAuth Google { get; set; }
        public FacebookAuth Facebook { get; set; }
    }

    public class GoogleAuth
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class FacebookAuth
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }

    public class OAuth
    {
        public string CallbackUrl { get; set; }
        public string ClientRedirectUrl { get; set; }
    }
}
