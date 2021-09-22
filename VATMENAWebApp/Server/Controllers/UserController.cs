using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VATMENAWebApp.Server.Data;
using VATMENAWebApp.Shared.Controllers;
using VATMENAWebApp.Shared.Models.Permissions;
using VATMENAWebApp.Shared.Models.VATSIM;
using VATMENAWebApp.Shared.Models.VATSIM.Divisions;
using VATMENAWebApp.Shared.Models.VATSIM.MemberData;
using VATMENAWebApp.Shared.Models.VATSIM.Ratings;
using VATMENAWebApp.Shared.Models.VATSIM.Regions;
using VATMENAWebApp.Shared.Models.VATSIM.SubDivisions;
using VATMENAWebApp.Shared.PageModels;

namespace VATMENAWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly VatmenaDbContext _context;
        private VatsimAuthManager vatsimAuthManager;

        public UserController(VatmenaDbContext context)
        {
            _context = context;
            vatsimAuthManager = new VatsimAuthManager();
        }

        [HttpGet]
        public async Task<CurrentUser> Get()
        {
            string id = Request.Cookies["vatsim_cid"];
            string token = Request.Cookies["auth_token"];
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(token))
            {
                VatsimUserDetails vatsimUserDetails = await this.vatsimAuthManager.GetUserDetails(token);
                if (vatsimUserDetails == null)
                    return null;

                VatsimMember vatsimMember = await _context.VatsimMembers.FirstOrDefaultAsync(vMember => vMember.Id == vatsimUserDetails.data.cid);
                PersonalDetail personalDetails = await _context.PersonalDetails.FirstOrDefaultAsync(pDetails => pDetails.Id == vatsimUserDetails.data.cid);
                UserPermission userPermission = await _context.UserPermissions.FirstOrDefaultAsync(uPermissions => uPermissions.Id == vatsimUserDetails.data.cid);

                if (vatsimMember == null || userPermission == null)
                    return null;

                VatsimRegion userRegion = null;
                VatsimDivision userDivision = null;
                VatsimSubDivision userSubDivision = null;
                PilotRating userPilotRating = await _context.PilotRatings.FirstOrDefaultAsync(pRating => pRating.Id == vatsimMember.PilotRating);
                ATCRating userATCRating = await _context.ATCRatings.FirstOrDefaultAsync(aRating => aRating.Id == vatsimMember.Rating);




                //Get the user Region, Division and SubDivision details
                if(vatsimUserDetails.data.vatsim.region != null)
                    userRegion = await _context.VatsimRegions.FirstOrDefaultAsync(uRegion => uRegion.Id == vatsimUserDetails.data.vatsim.region.id);

                if (vatsimUserDetails.data.vatsim.division != null)
                    userDivision = await _context.VatsimDivisions.FirstOrDefaultAsync(uDivision => uDivision.Id == vatsimUserDetails.data.vatsim.division.id);

                if(vatsimUserDetails.data.vatsim.subdivision != null)
                    userSubDivision = await _context.VatsimSubDivisions.FirstOrDefaultAsync(uSubDivision => uSubDivision.Code == vatsimUserDetails.data.vatsim.subdivision.id.ToString());

                CurrentUser currentUser = new CurrentUser
                {
                    Member = vatsimMember,
                    PilotRating = userPilotRating,
                    ATCRating = userATCRating,
                    Region = userRegion,
                    Division = userDivision,
                    SubDivision = userSubDivision,
                    PersonalDetails = personalDetails,
                    Permissions = userPermission
                };

                return currentUser;
            }

            return null;
        }

    }
}
