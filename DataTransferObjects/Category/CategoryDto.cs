using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Category
{
    public class CategoryDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [StringLength(150, ErrorMessage = "Category has maximum 150 characters")]
        [Required]
        [Display(Name ="Category")]
        public string Category { get; set; }
       
    }
}
