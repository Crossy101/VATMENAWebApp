using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATMENAWebApp.Shared.Models.VATSIM.Divisions;
using VATMENAWebApp.Shared.Models.VATSIM.MemberData;
using VATMENAWebApp.Shared.Models.VATSIM.SubDivisions;

namespace VATMENAWebApp.Shared.PageModels.Division
{
    public class ViewDivisionPageModel
    {
        public VatsimDivision CurrentDivision { get; set; }
        public List<VatsimSubDivision> SubDivisions { get; set; }
        public List<VatsimMember> DivisionMembers { get; set; }
    }
}
