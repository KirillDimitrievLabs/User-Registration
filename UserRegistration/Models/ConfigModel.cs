using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Components.Core;

namespace UserRegistration.Models
{
    public class TokenAuth
    {
        public readonly string AuthType = nameof(TokenAuth);
        public string ConnectionName { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }

        public TokenAuth GetConfig()
        {
            return new TokenAuth();
        }
    }

    public class GraphAPIAuth
    {
        public readonly string AuthType = nameof(GraphAPIAuth);
        public string ConnectionName { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public GraphAPIAuth GetConfig()
        {
            return new GraphAPIAuth();
        }
    }
}
