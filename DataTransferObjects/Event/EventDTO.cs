using DataTransferObjects.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Event
{
    public class EventDTO
    {

        [Display(Name = "id")]
        public int id { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int Category { get; set; }
        [Required]
        [Display(Name = "Event")]
        public int Event { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Date")]
        public string Date { get; set; }
        [Display(Name = "Hour")]
        public string hour { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        //Drop-down Lists
       
        public List<CategoryDto> eventCategoryList { get; set; }

        
    }
}
