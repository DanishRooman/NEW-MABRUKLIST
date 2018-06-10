using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Title { get; set; }
        public int SubTitle { get; set; }
        public int Neighbourhood { get; set; }
        public int Address { get; set; }
        public string Apartment { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
        public string Foreign { get; set; }
        public int Group { get; set; }
        public string Observation { get; set; }
        public string Children { get; set; }
        public string SingleSurname { get; set; }
        public string Blood { get; set; }
        public bool Doner { get; set; }
        public string Relationship { get; set; }
        public string Image { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}
