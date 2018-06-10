using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Categories
{
   public class CategoriesDTO
    {
        [Display(Name ="id")]
        public int id { get; set; }
        [Required]

        [Display(Name ="Category")]

        public string Category { get; set; }
    }
}
