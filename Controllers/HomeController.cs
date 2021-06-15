﻿using System;
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
                // TempData["error"] = "Please re-login, your session finished.";
               // return RedirectToAction("Index", "Auth");
            }

            return View(_inventoryContext.Items.ToList());
        }
    
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult AddEditItem(
            int id, 
            string tradeName, 
            string scientificName,
            string category,
            string manufactures, 
            string dangerousLevel,
            string packing,
            int price, 
            int quantity,
            bool isActive)
        {
            if (id > 0)
            {
                // update existence
                var item = _inventoryContext.Items.FirstOrDefault(p => p.Id == id);
                if (item != null)
                {
                    item.TradeName = tradeName;
                    item.ScientificName = scientificName;
                    item.Category = category;
                    item.Manufactures = manufactures;
                    item.DangerousLevel = dangerousLevel;
                    item.Packing = packing;
                    item.Price = price;
                    item.Quantity = quantity;
                    item.IsActive = isActive;
                }

            }
            else
            {
                // add new
                _inventoryContext.Items.Add(new Item()
                {
                    TradeName = tradeName,
                    ScientificName = scientificName,
                    Category = category,
                    Manufactures = manufactures,
                    DangerousLevel = dangerousLevel,
                    Packing = packing,
                    Price = price,
                    Quantity = quantity,
                    IsActive = isActive
                });
            }
            _inventoryContext.SaveChanges();  
            return RedirectToAction("Index");
        }

        public IActionResult RemoveItem(int id)
        {
            var item = _inventoryContext.Items.SingleOrDefault(p => p.Id == id);
            if (item == null) return RedirectToAction("Index");
            _inventoryContext.Items.Remove(item);
            _inventoryContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}