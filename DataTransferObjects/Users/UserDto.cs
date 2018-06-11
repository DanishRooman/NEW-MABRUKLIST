using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Users
{
    public class UserDto
    {
        [Display(Name ="id")]
        public int Id { get; set; }
        [Display(Name ="FirstName")]
        public string FirstName { get; set; }
        [Display(Name ="LastName")]
        public string LastName { get; set; }
        [Display(Name ="Title")]
        public int Title { get; set; }
        [Display(Name ="SubTitle")]
        public int SubTitle { get; set; }
        [Display(Name ="Neighbourhood")]
        public int Neighbourhood { get; set; }
        [Display(Name ="Address")]
        public int Address { get; set; }
        [Display(Name ="Apartment")]
        public string Apartment { get; set; }
        [Display(Name ="Company")]
        public string Company { get; set; }
        [Display(Name ="Phone")]
        public string Phone { get; set; }
        [Display(Name ="Cell phone")]
        public string Cellphone { get; set; }
        [Display(Name ="Foreign")]
        public string Foreign { get; set; }
        [Display(Name ="Group")]
        public int Group { get; set; }
        [Display(Name ="Observation")]
        public string Observation { get; set; }
        [Display(Name ="Children")]
        public string Children { get; set; }
        [Display(Name ="Single Surname")]
        public string SingleSurname { get; set; }
        [Display(Name ="Blood")]
        public string Blood { get; set; }
        [Display(Name ="Donor")]
        public bool Donor { get; set; }
        [Display(Name ="Relation Ship")]
        public string Relationship { get; set; }
        [Display(Name ="Image")]
        public string Image { get; set; }
        [Display(Name ="User")]
        public string User { get; set; }
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Display(Name ="Alternate Email")]
        public string AlternateEmail { get; set; }
        [Display(Name ="Password")]
        public string Password { get; set; }
        [Display(Name ="Active")]
        public bool Active { get; set; }
    }
}
