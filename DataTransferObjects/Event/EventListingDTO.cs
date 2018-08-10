using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Event
{
    public class EventListingDTO
    {
        [Display(Name = "Id")]
        public int id { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Date")]
        public string Date { get; set; }
        public string Address { get; set; }
        [Display(Name = "Guests Invited")]
        public string Invited { get; set; }
        [Display(Name = "Guests Standby")]
        public string standby { get; set; }
        public string data_tt_id { get; set; }
        public string data_tt_parent_id { get; set; }
    }
}
