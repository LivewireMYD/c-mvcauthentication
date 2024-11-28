using mvcmodels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace mvcmodels.Controllers
{
    public class AccountController : Controller
    {
        private mvcmodelsContext db = new mvcmodelsContext();

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var users = db.Users.ToList();
            System.Diagnostics.Debug.WriteLine("_________________________________________________");
            // Print user credentials to the console (for debugging purposes only)
            foreach (var use in users)
            {
                System.Diagnostics.Debug.WriteLine($"Email: {use.Email}, Password: {use.Password}");
            }
            // Query the database to find a matching user
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Login successful
                FormsAuthentication.SetAuthCookie(email, false);
                return RedirectToAction("Dashboard", "Account");
            }

            ViewBag.ErrorMessage = "Invalid email or password.";
            return View();
        }
       
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}