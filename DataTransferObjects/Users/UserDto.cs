﻿using DataAccessLayer.DBContext;
using DataTransferObjects.Address;
using DataTransferObjects.Group;
using DataTransferObjects.Neighbourhood;
using DataTransferObjects.Roles;
using DataTransferObjects.Subtitle;
using DataTransferObjects.Title;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataTransferObjects.Users
{
    public class UserDto
    {
        [Display(Name = "id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Title")]
        public int Title { get; set; }
        [Display(Name = "SubTitle")]
        public int SubTitle { get; set; }
        [Display(Name = "Neighbourhood")]
        public int Neighbourhood { get; set; }
        [Display(Name = "Address")]
        public int Address { get; set; }
        [Display(Name = "Apartment")]
        public string Apartment { get; set; }
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Cell phone")]
        public string Cellphone { get; set; }
        [Display(Name = "Foreign")]
        public string Foreign { get; set; }
        [Display(Name = "Group")]
        public int Group { get; set; }
        [Display(Name = "Observation")]
        public string Observation { get; set; }
        [Display(Name = "Children")]
        public string Children { get; set; }
        [Display(Name = "Single Surname")]
        public string SingleSurname { get; set; }
        [Display(Name = "Blood")]
        public string Blood { get; set; }
        [Display(Name = "Donor")]
        public bool Donor { get; set; }
        [Display(Name = "Relationship")]
        public string Relationship { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Display(Name = "Username")]
        [Required]
        [ScaffoldColumn(false)]
        [RegularExpression(@"^[^\s\,]+$", ErrorMessage = "Username Cannot Have Spaces")]
        public string User { get; set; }
        [Required]
        [Display(Name = "Email")]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(150, ErrorMessage = "Must be between 8 and 150 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(150, ErrorMessage = "Must be between 8 and 150 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Status")]
        public bool Active { get; set; }


        //Drop-down Lists
        public List<TitleDTO> titleList { get; set; }
        public List<SubtitleDTO> subtitleList { get; set; }
        public List<NeighbourhoodDTO> NeighbourhoodList { get; set; }
        public List<AddressDTO> AddressList { get; set; }
        public List<GroupDTO> GroupList { get; set; }
        public List<RolesDTO> RolesList { get; set; }

    }

}
