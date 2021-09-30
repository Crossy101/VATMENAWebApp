using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATMENAWebApp.Shared.Models.VATSIM.MemberData
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class VatsimMember
    {
        [Key]
        [MaxLength(10)]
        public string Id { get; set; }
        public string FullName { get; set; }
        public int Rating { get; set; }
        [MaxLength(3)]
        public int PilotRating { get; set; }
        public DateTime? Susp_date { get; set; }
        public DateTime Reg_date { get; set; }
        [MaxLength(5)]
        [ForeignKey("FK_VatsimRegion")]
        public string Region { get; set; }
        [MaxLength(5)]
        [ForeignKey("FK_VatsimDivision")]
        public string Division { get; set; }
        [MaxLength(8)]
        [ForeignKey("FK_VatsimSubDivision")]
        public string SubDivision { get; set; }
        public DateTime LastRatingChange { get; set; }
    }
}
