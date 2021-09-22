using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATMENAWebApp.Shared.Models.VATSIM.Divisions
{
    public class VatsimDivision
    {
        [Key]
        [MaxLength(5)]
        public string Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [ForeignKey("FK_ParentRegion")]
        [MaxLength(5)]
        public string ParentRegion { get; set; }
        [MaxLength(1)]
        public int SubDivisionAllowed { get; set; }
    }
}
