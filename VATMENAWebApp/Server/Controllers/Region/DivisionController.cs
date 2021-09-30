using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VATMENAWebApp.Server.Data;
using VATMENAWebApp.Shared.Models.VATSIM.Divisions;
using VATMENAWebApp.Shared.Models.VATSIM.MemberData;
using VATMENAWebApp.Shared.Models.VATSIM.SubDivisions;
using VATMENAWebApp.Shared.PageModels.Division;

namespace VATMENAWebApp.Server.Controllers.Region
{
    [ApiController]
    [Route("{controller}")]
    public class DivisionController : ControllerBase
    {
        private readonly ILogger<DivisionController> _logger;
        private readonly VatmenaDbContext _context;

        public DivisionController(ILogger<DivisionController> logger, VatmenaDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// This function gets details of a division with members and subdivisions included
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns>Task<ViewDivisionPageModel></returns>
        [HttpGet]
        public async Task<ViewDivisionPageModel> Get(string divisionId)
        {
            try
            {
                ViewDivisionPageModel viewDivisionPageModel = new ViewDivisionPageModel();

                VatsimDivision currentDivision = await _context.VatsimDivisions.FirstOrDefaultAsync(vDivision => vDivision.Id == divisionId);
                if (currentDivision == null)
                    return viewDivisionPageModel;

                List<VatsimSubDivision> subDivisions = await _context.VatsimSubDivisions.Where(vSubDivision => vSubDivision.ParentDivision == currentDivision.Id).ToListAsync();
                List<VatsimMember> divisionMembers = await _context.VatsimMembers.Where(vMember => vMember.Division == currentDivision.Id).ToListAsync();

                viewDivisionPageModel.CurrentDivision = currentDivision;
                viewDivisionPageModel.SubDivisions = subDivisions;
                viewDivisionPageModel.DivisionMembers = divisionMembers;

                return viewDivisionPageModel;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
