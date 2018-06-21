using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Roles
{
    public class RolesDTO
    {
        [Display(Name ="id")]
        public string id { get; set; }
        [Required]
        [Display(Name ="Roles")]
        public string Roles { get; set; }

    }
}
