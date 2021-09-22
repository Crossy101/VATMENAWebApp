using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VATMENAWebApp.Server.Data;
using VATMENAWebApp.Shared.Controllers;
using VATMENAWebApp.Shared.Models.VATSIM.Divisions;
using VATMENAWebApp.Shared.Models.VATSIM.MetarData;
using VATMENAWebApp.Shared.Models.VATSIM.Regions;
using VATMENAWebApp.Shared.Models.VATSIM.ServerData;
using VATMENAWebApp.Shared.Models.VATSIM.SubDivisions;

namespace VATMENAWebApp.Server.Controllers.Vatsim
{
    [ApiController]
    [Route("[controller]")]
    public class VatsimController : ControllerBase
    {
        private readonly ILogger<VatsimController> _logger;
        private readonly VatmenaDbContext _context;
        private HttpClient httpClient;

        public VatsimController(ILogger<VatsimController> logger, VatmenaDbContext context)
        {
            httpClient = new HttpClient();
            _context = context;
            _logger = logger;
        }

        [HttpGet("Data/Metar")]
        public async Task<List<AirportMetar>> GetMetar(string search)
        {
            VatsimDataManager vatsimDataManager = new VatsimDataManager();
            List<AirportMetar> list = await vatsimDataManager.GetLatestWeather(search);
            return list;
        }

        [HttpGet("Data/LoadRegions")]
        public async Task LoadVatsimRegions()
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://api.vatsim.net/api/regions/");
            string data = await response.Content.ReadAsStringAsync();

            List<VatsimRegion> vatsimRegions = JsonConvert.DeserializeObject<List<VatsimRegion>>(data);

            _context.VatsimRegions.AddRange(vatsimRegions);
            await _context.SaveChangesAsync();
        }

        [HttpGet("Data/LoadDivisions")]
        public async Task LoadVatsimDivisions()
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://api.vatsim.net/api/divisions/");
            string data = await response.Content.ReadAsStringAsync();

            List<VatsimDivision> vatsimDivisions = JsonConvert.DeserializeObject<List<VatsimDivision>>(data);

            _context.VatsimDivisions.AddRange(vatsimDivisions);
            await _context.SaveChangesAsync();
        }

        [HttpGet("Data/LoadSubDivisions")]
        public async Task LoadVatsimSubDivisions()
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://api.vatsim.net/api/subdivisions/");
            string data = await response.Content.ReadAsStringAsync();

            List<VatsimSubDivision> vatsimSubDivisions = JsonConvert.DeserializeObject<List<VatsimSubDivision>>(data);

            _context.VatsimSubDivisions.AddRange(vatsimSubDivisions);
            await _context.SaveChangesAsync();
        }

        [HttpGet("Data/LoadRatings")]
        public async Task LoadRatings()
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://data.vatsim.net/v3/vatsim-data.json");
            string data = await response.Content.ReadAsStringAsync();

            LiveVatsimData liveVatsimData = JsonConvert.DeserializeObject<LiveVatsimData>(data);

            await _context.ATCRatings.AddRangeAsync(liveVatsimData.ratings);
            await _context.PilotRatings.AddRangeAsync(liveVatsimData.pilot_ratings);
            await _context.SaveChangesAsync();
        }
    }
}
