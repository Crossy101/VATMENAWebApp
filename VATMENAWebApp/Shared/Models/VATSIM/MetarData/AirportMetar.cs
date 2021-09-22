using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATMENAWebApp.Shared.Models.VATSIM.MetarData
{
    public class AirportMetar
    {
        public string ICAO { get; set; }
        public string Metar { get; set; }

        public AirportMetar() { }

        public AirportMetar(string metar_string)
        {
            List<string> values = metar_string.Split(' ').ToList();
            this.ICAO = values[0];
            this.Metar = metar_string;
        }
    }
}
