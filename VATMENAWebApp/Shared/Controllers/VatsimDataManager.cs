using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VATMENAWebApp.Shared.Models.VATSIM.MetarData;

namespace VATMENAWebApp.Shared.Controllers
{
    public class VatsimDataManager
    {
        public string vatsimDataURL = "https://data.vatsim.net/v3/vatsim-data.json";
        public string vatsimMetarURL = "https://metar.vatsim.net/";

        public VatsimDataManager() 
        { }

        public async Task<List<AirportMetar>> GetLatestWeather(string search)
        {
            try
            {
                List<AirportMetar> airportMetars = new List<AirportMetar>();
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage responseMessage = await httpClient.GetAsync(vatsimMetarURL + search);
                string data = await responseMessage.Content.ReadAsStringAsync();

                string[] airportMetar = data.Split("\n");
                foreach (var currMetar in airportMetar)
                {
                    airportMetars.Add(new AirportMetar(currMetar));
                }

                return airportMetars;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
