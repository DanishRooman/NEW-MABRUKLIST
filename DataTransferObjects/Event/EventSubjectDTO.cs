using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Event
{
    public class EventSubjectDTO
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        [Display(Name ="Subject")]
        public string EventSubject { get; set; }
    }
}
