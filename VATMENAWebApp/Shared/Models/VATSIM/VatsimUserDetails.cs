using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATMENAWebApp.Shared.Models.VATSIM
{
    public class VatsimUserDetails
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public string cid { get; set; }
        public Personal personal { get; set; }
        public Vatsim vatsim { get; set; }
        public Oauth oauth { get; set; }
    }

    public class Country
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Personal
    {
        public string name_first { get; set; }
        public string name_last { get; set; }
        public string name_full { get; set; }
        public string email { get; set; }
        public Country country { get; set; }
    }

    public class Rating
    {
        public int id { get; set; }
        public string @long { get; set; }
        public string @short { get; set; }
    }

    public class Pilotrating
    {
        public int id { get; set; }
        public string @long { get; set; }
        public string @short { get; set; }
    }

    public class Division
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Region
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Subdivision
    {
        public object id { get; set; }
        public object name { get; set; }
    }

    public class Vatsim
    {
        public Rating rating { get; set; }
        public Pilotrating pilotrating { get; set; }
        public Division division { get; set; }
        public Region region { get; set; }
        public Subdivision subdivision { get; set; }
    }

    public class Oauth
    {
        public string token_valid { get; set; }
    }
}
