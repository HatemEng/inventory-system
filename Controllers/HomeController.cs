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
        private readonly InventoryContext _inventoryContext;

        public HomeController(ILogger<HomeController> logger, InventoryContext inventoryContext)
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

            return View();
        }
    }
}