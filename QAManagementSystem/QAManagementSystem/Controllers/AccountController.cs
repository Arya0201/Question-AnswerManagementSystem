using QAManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QAManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        private QAManagementSystemEntities context = new QAManagementSystemEntities();
        public ActionResult Login()
        {
            // Check if session is expired
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();

                // Redirect user to appropriate dashboard based on role
                if (role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (role == "Teacher")
                {
                    return RedirectToAction("Index", "Teacher");
                }
                else if (role == "Student")
                {
                    return RedirectToAction("Index", "Student");
                }
            }

            // If session is not expired or role is not set, show login page
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password, string Role)
        {
            // Find the user in the database based on email and password
            var foundUser = context.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password && u.Role==Role);

            if (foundUser != null)
            {
                Session["Role"] = Role;
                Session["UserId"] = foundUser.UserId;
                Session["Username"] = foundUser.Username;
                Session["Email"] = foundUser.Email;
                FormsAuthentication.SetAuthCookie(foundUser.Username, false);

                if (foundUser.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (foundUser.Role == "Teacher")
                {
                    return RedirectToAction("Index", "Teacher");
                }
                else if (foundUser.Role == "Student")
                {
                    return RedirectToAction("Index", "Student");
                }
                
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                // User not found or invalid credentials, handle accordingly (e.g., show error message)
            }

            // If login fails, return to login view
            return View();
        }

        public ActionResult Logout()
        {
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null))
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }

            // Clear session variables
            Session.Clear();
            Session.Abandon();

            FormsAuthentication.SignOut();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            return RedirectToAction("Login");

        }



    }
}