using System;
using System.Linq;
using Inventory.Models;
using Inventory.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    public class OrdersController : Controller
    {
        private readonly InventoryContext _inventoryContext;

        public OrdersController(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        // GET
        public IActionResult Index()
        {
            ViewBag.user_name = HttpContext.Session.GetString("user_name");
            if (string.IsNullOrWhiteSpace(ViewBag.user_name))
            {
                // TempData["error"] = "Please re-login, your session finished.";
                return RedirectToAction("Index", "Auth");
            }
            ViewBag.accounType = HttpContext.Session.GetString("account_type");
            return View(_inventoryContext.Orders
                .Include(p => p.Customer)
                .Include(p => p.Item).ToList());
        }


        public IActionResult AddNew(int quantity, int itemId)
        {
            var customer = _inventoryContext.Customers.SingleOrDefault(p => p.UserId == HttpContext.Session.GetInt32("user_id"));
            var item = _inventoryContext.Items.SingleOrDefault(p => p.Id == itemId);
            if (customer != null && item != null)
            {
                // add new
                _inventoryContext.Orders.Add(new Order()
                {
                    Quantity = quantity,
                    Customer = customer,
                    Item = item,
                    Date = DateTime.Now,
                    Status = Statics.Pending
                });
            }
          
            _inventoryContext.SaveChanges();  
            return RedirectToAction("Index", "Items");
        }

        public IActionResult Status(int id, string status)
        {
            var order = _inventoryContext.Orders.FirstOrDefault(p => p.Id == id);

            if (order == null) return RedirectToAction("Index");
            if (status == Statics.Approve)
            {
                var item = _inventoryContext.Items.FirstOrDefault(p => p.Id == order.ItemId);
                if (item != null && item.Quantity >= order.Quantity)
                {
                    item.Quantity -= order.Quantity;
                }
                else
                {
                    status = Statics.Reject;
                }
            }
            order.Status = status;
            _inventoryContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}