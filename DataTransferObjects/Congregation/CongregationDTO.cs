using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Congregation
{
   public class CongregationDTO
    {
        [Display(Name ="id")]
        public int id { get; set; }
        [Required]

        [Display(Name ="Congregation")]
        public string congregation { get; set; }
    }
}
