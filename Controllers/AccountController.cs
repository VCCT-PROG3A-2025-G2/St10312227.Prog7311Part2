using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AgriEnergyConnect.Models;
using AgriEnergyConnect.ViewModels;

namespace AgriEnergyConnect.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // If already logged in, redirect based on role
            var role = HttpContext.Session.GetString("Role");
            if (!string.IsNullOrEmpty(role))
            {
                if (role == "Employee")
                    return RedirectToAction("Dashboard", "Employee");
                else if (role == "Farmer")
                    return RedirectToAction("Dashboard", "Farmer");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string hashedPassword = HashPassword(model.Password);

            var user = _context.Users
                .FirstOrDefault(u => u.Username == model.Username && u.PasswordHash == hashedPassword);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View(model);
            }

            // Set session
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            // Set login success flag for inline animation
            ViewBag.LoginSuccess = true;

            // Instead of redirecting here, let the view trigger redirect via JavaScript
            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterEmployee(EmployeeRegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ViewBag.Error = "Username already exists.";
                return View(model);
            }

            // Create user
            var user = new User
            {
                Username = model.Username,
                Role = "Employee",
                PasswordHash = HashPassword(model.Password)
            };
            _context.Users.Add(user);

            // Create employee profile
            var employee = new Employee
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Username = model.Username,
                Role = "Employee"
            };
            _context.Employees.Add(employee);

            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
