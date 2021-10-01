using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATMENAWebApp.Shared.Enums.Admin;

namespace VATMENAWebApp.Shared.Models.Admin
{
    public class StaffPermission
    {
        [Key]
        public int Id { get; set; }
        public string StaffPosition { get; set; }
        public bool SiteAdmin { get; set; }
        public bool RegionStaff { get; set; }
        public bool DivisionStaff { get; set; }
        public bool SubDivisionStaff { get; set; }
        public AccountPermissions AccountPermissions { get; set; }
        public EventPermissions EventPermissions { get; set; }
        public RatingPermissions RatingPermissions { get; set; }
    }
}
