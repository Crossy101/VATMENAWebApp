using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATMENAWebApp.Shared.Models.VATSIM.Regions
{
    public class VatsimRegion
    {

        [Key]
        [MaxLength(8)]
        public string Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(10)]
        public string Director { get; set; }
    }
}
