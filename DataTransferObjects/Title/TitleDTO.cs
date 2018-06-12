using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects.Group;

namespace DataTransferObjects.Title
{
    public class TitleDTO
    {
        [Display(Name = "Id")]
        public int id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

    }
}
