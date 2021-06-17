using System;
using System.Security.Cryptography;
using System.Text;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Persistence
{
    public class InventoryContext: DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // add default users to the system
            builder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    IsActive = true,
                    Name = "hussain",
                    Password = HashPassword("12345")
                },
                new User()
                {
                    Id = 2,
                    IsActive = true,
                    Name = "hatem",
                    Password = HashPassword("12345")
                },
                new User()
                {
                    Id = 3,
                    Name = "ali",
                    Password = HashPassword("12345"),
                    IsActive = true
                }
            );
            // add default employees to the system
            builder.Entity<Employee>().HasData(
                new Employee()
                {
                    Id = 1,
                    FirstName = "Hussain", 
                    LastName = "Nasir", 
                    Address = "Baghdad, Karada",
                    Email = "test@test.com",
                    Gender = "Male",
                    Mobile = "07XXXXXXXXX",
                    Role = Statics.Admin,
                    Salary = 1000,
                    BirthDate = Convert.ToDateTime("01/01/1996"),
                    UserId = 1
                },
                new Employee()
                {
                    Id = 2,
                    FirstName = "Hatem", 
                    LastName = "karim", 
                    Address = "Baghdad, Al-Sadir City",
                    Email = "test@test.com",
                    Gender = "Male",
                    Mobile = "07XXXXXXXXX",
                    Role = Statics.Admin,
                    Salary = 1000,
                    BirthDate = Convert.ToDateTime("01/01/1996"),
                    UserId = 2
                }
            );
            // add default customers to the system
            builder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = 1,
                    FirstName = "Ali",
                    LastName = "Ahamad",
                    Address = "Baghdad",
                    City = "Baghdad",
                    Email = "test@test.com",
                    Mobile = "07XXXXXXXXX",
                    PharmacyName = "T-Pharmacy",
                    UserId = 3
                }    
            );

        }
        // sha256 encryption
        public string HashPassword(string password)
        {
            var hash = new StringBuilder();
            var crypto = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (var theByte in crypto) hash.Append(theByte.ToString("x2"));
            return hash.ToString();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
    
}