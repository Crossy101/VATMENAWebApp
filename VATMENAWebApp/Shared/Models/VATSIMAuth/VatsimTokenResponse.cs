using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATMENAWebApp.Shared.Models.VATSIMAuth
{
    public class VatsimTokenResponse
    {
        public List<string> scopes { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }

        public VatsimTokenResponse() { }
        public VatsimTokenResponse(string auth_token) 
        {
            this.access_token = auth_token;
        }
    }
}
