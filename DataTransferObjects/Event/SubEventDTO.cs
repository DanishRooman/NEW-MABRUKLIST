using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Event
{
    public class SubEventDTO
    {
        [Display(Name="ID")]
        public int eventID { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        [Display(Name ="Date")]
        public string EventDate { get; set; }
        public string Address { get; set; }
    }
}
