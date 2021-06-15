using System.Linq;
using Inventory.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    public class AuthController : Controller
    {
        private readonly InventoryContext _inventoryContext;

        public AuthController(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        [Route("login")]
        // GET
        public IActionResult Index()
        {
            ViewBag.error = TempData["error"] as string; 
            return View();
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
            return TempData["error"] == null ? RedirectToAction("Index", "Home") : RedirectToAction("Index");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user_name");
            HttpContext.Session.Remove("user_id");
            return RedirectToAction("Index");
        }
    }
}