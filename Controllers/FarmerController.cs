using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using AgriEnergyConnect.Models;

namespace AgriEnergyConnect.Controllers
{
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            var farmer = _context.Farmers.FirstOrDefault(f => f.Username == username);
            if (farmer == null)
                return RedirectToAction("Login", "Account");

            var products = _context.Products
                .Where(p => p.FarmerId == farmer.Id)
                .ToList();

            return View(products); // Views/Farmer/Dashboard.cshtml
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View(); // Views/Farmer/AddProduct.cshtml
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            var farmer = _context.Farmers.FirstOrDefault(f => f.Username == username);
            if (farmer == null)
                return RedirectToAction("Login", "Account");

            product.FarmerId = farmer.Id;

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }
    }
}
