using System;
using System.Linq;
using Inventory.Models;
using Inventory.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly InventoryContext _inventoryContext;

        public EmployeesController(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        
        public IActionResult Index()
        {
            ViewBag.user_name = HttpContext.Session.GetString("user_name");
            if (string.IsNullOrWhiteSpace(ViewBag.user_name))
            {
                // TempData["error"] = "Please re-login, your session finished.";
                return RedirectToAction("Index", "Auth");
            }
            ViewBag.accounType = HttpContext.Session.GetString("account_type");
            return View(_inventoryContext.Employees.Include(p => p.User).ToList());
        }
        public IActionResult AddEdit(
            int id, 
            string username, 
            string password, 
            string firstName, 
            string lastName,
            string birthDate,
            string gender, 
            string address,
            string mobile,
            string email, 
            int salary,
            string role)
        {
            if (id > 0)
            {
                // update existence
                var employee = _inventoryContext.Employees.FirstOrDefault(p => p.Id == id);
                if (employee != null)
                {
                    employee.FirstName = firstName;
                    employee.LastName = lastName;
                    employee.BirthDate = Convert.ToDateTime(birthDate);
                    employee.Gender = gender;
                    employee.Address = address;
                    employee.Mobile = mobile;
                    employee.Email = email;
                    employee.Salary = salary;
                    employee.Role = role;
                    var user = _inventoryContext.Users.FirstOrDefault(p => p.Id == employee.UserId);
                    if (user != null)
                    {
                        user.Name = username;
                        if (string.IsNullOrWhiteSpace(password))
                        {
                            user.Password = _inventoryContext.HashPassword(password);
                        }
                    }
                }

            }
            else
            {
                // add new
                _inventoryContext.Employees.Add(new Employee()
                {
                    User = new User()
                    {
                        Name = username,
                        Password = _inventoryContext.HashPassword(password),
                        IsActive = true
                    },
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDate = Convert.ToDateTime(birthDate),
                    Gender = gender,
                    Address = address,
                    Mobile = mobile,
                    Email = email,
                    Salary = salary,
                    Role = role,
                });
            }
            _inventoryContext.SaveChanges();  
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            var employee = _inventoryContext.Employees.Include(p => p.User).SingleOrDefault(p => p.Id == id);
            if (employee == null) return RedirectToAction("Index");
            _inventoryContext.Employees.Remove(employee);
            _inventoryContext.Users.Remove(employee.User);
            _inventoryContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}