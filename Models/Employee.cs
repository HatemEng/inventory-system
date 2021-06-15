using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string Role { get; set; }
    }
}