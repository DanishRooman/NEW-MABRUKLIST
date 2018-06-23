using DataTransferObjects.Congregation;
using DataTransferObjects.Group;
using DataTransferObjects.Neighbourhood;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.ChooseEvent
{
    public class ChooseEventDTO
    {
        [Display(Name ="id")]
        public int id { get; set; }
        [Required]
        [Display(Name ="Choose Group")]
        public int Group { get; set; }
        [Required]
        [Display(Name ="Choose Congregation")]
        public int Congregation { get; set; }
        [Required]
        [Display(Name ="Choose Neighbourhood")]
        public int Neighbourhood { get; set; }


        //Drop-down Lists
    
        public List<GroupDTO> groupList { get; set; }
        public List<CongregationDTO> congregationList { get; set; }
        public List<NeighbourhoodDTO> neighbourhoodList { get; set; }

    }
}
