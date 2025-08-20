using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AdminUser model)
        {
            if (model.Username == "admin" && model.Password == "password")
            {
                HttpContext.Session.SetString("AdminUser", model.Username);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password";
            return View(model);
        }


        // GET: Admin/Index (Dashboard)
        public IActionResult Index()
        {
            var admin = HttpContext.Session.GetString("AdminUser");
            if (string.IsNullOrEmpty(admin))
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        // GET: Admin/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
