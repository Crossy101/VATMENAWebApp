using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATMENAWebApp.Shared.Models.Permissions
{
    public class UserPermission
    {
        [Key]
        public string Id { get; set; }
        public bool IsAdmin { get; set; }
        public bool AdminEvents { get; set; }
    }
}
