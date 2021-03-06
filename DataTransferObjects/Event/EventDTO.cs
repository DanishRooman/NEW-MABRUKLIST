﻿using DataTransferObjects.Category;
using DataTransferObjects.Types;
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
        [Display(Name = "Type")]
        public int EventFor { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Date")]
        public string Date { get; set; }
        [Display(Name = "Hour")]
        public string hour { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Description")]
        public string Comment { get; set; }

        //Drop-down Lists
       
        public List<CategoryDto> eventCategoryList { get; set; }
        public List<TypeDto> eventTypesList { get; set; }

        
    }
}
