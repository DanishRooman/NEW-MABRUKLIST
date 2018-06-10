using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Neighbourhood
{
    public class NeighbourhoodDTO
    {
        [Display(Name ="id")]
        public int id { get; set; }
        [Required]
        [Display(Name = "Neighbourhood")]
        public string Neighbourhood { get; set; }
    }
}
