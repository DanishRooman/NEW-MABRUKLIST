using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Group
{
    public class GroupDTO
    {
        public string group;

        [Display(Name = "id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Group")]
        public string Group { get; set; }
    }
}
