using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Neighborhood
{
    public class NeighborhoodDTO
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Required]

        [Display(Name = "Neighborhood")]
        public string Neighborhood { get; set; }
    }
}
