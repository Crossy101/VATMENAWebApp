using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VATMENAWebApp.Shared.Models.Permissions;
using VATMENAWebApp.Shared.Models.VATSIM.Divisions;
using VATMENAWebApp.Shared.Models.VATSIM.MemberData;
using VATMENAWebApp.Shared.Models.VATSIM.Ratings;
using VATMENAWebApp.Shared.Models.VATSIM.Regions;
using VATMENAWebApp.Shared.Models.VATSIM.SubDivisions;

namespace VATMENAWebApp.Server.Data
{
    public class VatmenaDbContext : DbContext
    {
        public VatmenaDbContext(DbContextOptions<VatmenaDbContext> options) : base(options)
        {

        }

        public DbSet<VatsimRegion> VatsimRegions { get; set; }
        public DbSet<VatsimDivision> VatsimDivisions { get; set; }
        public DbSet<VatsimSubDivision> VatsimSubDivisions { get; set; }
        public DbSet<VatsimMember> VatsimMembers { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<PersonalDetail> PersonalDetails { get; set; }
        public DbSet<PilotRating> PilotRatings { get; set; }
        public DbSet<ATCRating> ATCRatings { get; set; }
    }
}
