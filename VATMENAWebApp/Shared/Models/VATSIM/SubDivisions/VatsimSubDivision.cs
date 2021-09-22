using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATMENAWebApp.Shared.Models.VATSIM.SubDivisions
{
    public class VatsimSubDivision
    {
        [Key]
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string Fullname { get; set; }
        [ForeignKey("FK_ParentDivision")]
        [MaxLength(5)]
        public string ParentDivision { get; set; }
    }
}
