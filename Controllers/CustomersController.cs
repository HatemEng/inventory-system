using System;
using System.Linq;
using Inventory.Models;
using Inventory.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    public class CustomersController : Controller
    {
       private readonly InventoryContext _inventoryContext;

        public CustomersController(InventoryContext inventoryContext)
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
            return View(_inventoryContext.Customers.Include(p => p.User).ToList());
        }
        public IActionResult AddEdit(
            int id, 
            string username, 
            string password, 
            string firstName, 
            string lastName,
            string address,
            string city,
            string gender, 
            string mobile,
            string email, 
            string pharmacyName)
        {
            if (id > 0)
            {
                // update existence
                var customer = _inventoryContext.Customers.FirstOrDefault(p => p.Id == id);
                if (customer != null)
                {
                    customer.FirstName = firstName;
                    customer.LastName = lastName;
                    customer.City = city;
                    customer.Address = address;
                    customer.Mobile = mobile;
                    customer.Email = email;
                    customer.PharmacyName = pharmacyName;
                    var user = _inventoryContext.Users.FirstOrDefault(p => p.Id == customer.UserId);
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
                _inventoryContext.Customers.Add(new Customer()
                {
                    User = new User()
                    {
                        Name = username,
                        Password = _inventoryContext.HashPassword(password),
                        IsActive = true
                    },
                    FirstName = firstName,
                    LastName = lastName,
                    City = city,
                    Address = address,
                    Mobile = mobile,
                    Email = email,
                    PharmacyName = pharmacyName,
                });
            }
            _inventoryContext.SaveChanges();  
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            var employee = _inventoryContext.Customers.Include(p => p.User).SingleOrDefault(p => p.Id == id);
            if (employee == null) return RedirectToAction("Index");
            _inventoryContext.Customers.Remove(employee);
            _inventoryContext.Users.Remove(employee.User);
            _inventoryContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}