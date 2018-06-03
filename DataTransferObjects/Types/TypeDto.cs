using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Types
{
   public class TypeDto
    {
        [Display(Name ="id")]
        public int id { get; set; }

        [Required]
        [Display(Name ="Type")]
        public string Type { get; set; }
    }
}
