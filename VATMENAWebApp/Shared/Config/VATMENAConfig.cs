using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATMENAWebApp.Shared.Config
{
    public class VATMENAConfig
    {
        public string WebAppAddress { get; set; }
        public int VatsimClientId { get; set; }
        public string VatsimSecret { get; set; }
        public string VatsimAuthKey { get; set; }
    }
}
