using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATMENAWebApp.Shared.Enums.Admin
{
    public enum RatingPermissions : int
    {
        None = 0,
        Read = 1,
        Write = 2,
        All = 3
    }
}
