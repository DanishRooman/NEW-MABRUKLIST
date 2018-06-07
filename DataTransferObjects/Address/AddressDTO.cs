using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Address
{
    public class AddressDTO
    {
       [Display(Name ="id")]
        public int id { get; set; }
        [Required]
        [Display(Name ="Address")]
        public string Address { get; set; }
    }
}
