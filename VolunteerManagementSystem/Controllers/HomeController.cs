using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace VolunteerManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Check if admin is logged in
            var admin = HttpContext.Session.GetString("AdminUser");
            if (string.IsNullOrEmpty(admin))
            {
                // Not logged in, redirect to login
                return RedirectToAction("Login", "Admin");
            }

            // Logged in: show admin dashboard
            return View();
        }
    }
}
