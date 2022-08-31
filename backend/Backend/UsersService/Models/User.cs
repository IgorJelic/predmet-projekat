using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public string Role { get; set; }
        public string ProfileStatus { get; set; }
        public List<Order> CustomerOrders { get; set; }
        public List<Order> DelivererOrders { get; set; }
    }
}
