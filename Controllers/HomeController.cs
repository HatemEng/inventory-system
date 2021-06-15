using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Inventory.Models;
using Inventory.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InventoryContext _inventoryContext;

        public HomeController(ILogger<HomeController> logger, InventoryContext inventoryContext)
        {
            _logger = logger;
            _inventoryContext = inventoryContext;
        }
        
        public IActionResult Index()
        {
            ViewBag.user_name = HttpContext.Session.GetString("user_name");
            if (string.IsNullOrWhiteSpace(ViewBag.user_name))
            {
                TempData["error"] = "Please re-login, your session finished.";
                return RedirectToAction("Login");
            }
            return View();
        }
        [Route("login")]
        public IActionResult Login()
        {
            ViewBag.error = TempData["error"] as string; 
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        [Route("process-login")]
        public RedirectToActionResult ProcessLogin(string username, string password)
        {
            TempData["error"] = null;
            // check the user and password if empty
            if (string.IsNullOrWhiteSpace(username))
            {
                TempData["error"] = "Please enter a valid username.";
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                TempData["error"] = "Please don't leave the password empty.";
            }
            else
            {
                var cryptPassword = _inventoryContext.HashPassword(password);
                var user = _inventoryContext.Users
                    .FirstOrDefault(p => p.Name == username && p.Password == cryptPassword);
                // login failed
                if (user == null)
                {
                    TempData["error"] = "Invalid account.";
                }
                else
                {
                    // login successful save the sessions 
                    HttpContext.Session.SetInt32("user_id", user.Id);
                    HttpContext.Session.SetString("user_name", user.Name);
                }
            }
            // if there error then redirect to login page with error message else go to the home page 
            return RedirectToAction(TempData["error"] != null ? "Login" : "Index");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user_name");
            HttpContext.Session.Remove("user_id");
            return RedirectToAction("Login");
        }
    }
}