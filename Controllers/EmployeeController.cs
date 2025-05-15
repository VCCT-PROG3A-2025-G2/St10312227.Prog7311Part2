using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace AgriEnergyConnect.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Employee")
                return RedirectToAction("Login", "Account");

            var farmers = _context.Farmers.ToList();
            return View(farmers);
        }

        [HttpGet]
        public IActionResult ViewProductsByFarmer(int farmerId)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Employee")
                return RedirectToAction("Login", "Account");

            var farmer = _context.Farmers
                .Include(f => f.Products)
                .FirstOrDefault(f => f.Id == farmerId);

            if (farmer == null)
                return NotFound();

            return View(farmer);
        }

        [HttpGet]
        public IActionResult AddFarmer()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Employee")
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public IActionResult AddFarmer(FarmerProfileViewModel model)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Employee")
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(model);

            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ViewBag.Error = "Username already exists.";
                return View(model);
            }

            var user = new User
            {
                Username = model.Username,
                Role = "Farmer",
                PasswordHash = HashPassword(model.Password)
            };

            var farmer = new Farmer
            {
                Username = model.Username,
                Name = model.Name,
                Location = model.Location,
                ContactInfo = model.ContactInfo
            };

            _context.Users.Add(user);
            _context.Farmers.Add(farmer);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult ViewAllProducts(string farmerName, string productName, string productType, DateTime? startDate, DateTime? endDate)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Employee")
                return RedirectToAction("Login", "Account");

            // Get distinct filter values
            var farmers = _context.Farmers.Select(f => f.Name).Distinct().ToList();
            var productTypes = _context.Products.Select(p => p.Category).Distinct().ToList();

            // Pass to view via ViewData
            ViewData["Farmers"] = farmers;
            ViewData["ProductTypes"] = productTypes;

            var query = _context.Products
                .Include(p => p.Farmer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(farmerName))
                query = query.Where(p => p.Farmer.Name.Contains(farmerName));

            if (!string.IsNullOrEmpty(productName))
                query = query.Where(p => p.Name.Contains(productName));

            if (!string.IsNullOrEmpty(productType))
                query = query.Where(p => p.Category.Contains(productType));

            if (startDate.HasValue)
                query = query.Where(p => p.DateAdded >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(p => p.DateAdded <= endDate.Value);

            var products = query.ToList();
            return View(products);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}