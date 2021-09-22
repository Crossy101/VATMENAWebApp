using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATMENAWebApp.Shared.Models.VATSIMAuth
{
    public class VatsimTokenRequest
    {
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string redirect_uri { get; set; }
        public string code { get; set; }

        public VatsimTokenRequest(string authCode)
        {
            this.grant_type = "authorization_code";
            this.client_id = "866";
            this.client_secret = "tfuWe3n6vaLJQ5Nvf4X4iAMB6CIZEdugPywHXsRg";
            this.redirect_uri = "https://test.vatsim.me:44347/authentication";
            this.code = authCode;
        }
    }
}
