using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Subtitle
{
    public class SubtitleDTO
    {
        [Display(Name = "id")]
        public int id { get; set; }
        [Required]
        [Display(Name = "Subtitle")]
        public string Subtitle { get; set; }
    }
}
