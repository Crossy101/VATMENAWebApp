using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATMENAWebApp.Shared.Models.Permissions;
using VATMENAWebApp.Shared.Models.VATSIM.Divisions;
using VATMENAWebApp.Shared.Models.VATSIM.MemberData;
using VATMENAWebApp.Shared.Models.VATSIM.Ratings;
using VATMENAWebApp.Shared.Models.VATSIM.Regions;
using VATMENAWebApp.Shared.Models.VATSIM.SubDivisions;

namespace VATMENAWebApp.Shared.PageModels
{
    public class CurrentUser
    {
        public VatsimMember Member { get; set; }
        public ATCRating ATCRating { get; set; }
        public PilotRating PilotRating { get; set; }
        public VatsimRegion Region { get; set; }
        public VatsimDivision Division { get; set; }
        public VatsimSubDivision SubDivision { get; set; }
        public PersonalDetail PersonalDetails { get; set; }
        public UserPermission Permissions { get; set; }
    }
}
