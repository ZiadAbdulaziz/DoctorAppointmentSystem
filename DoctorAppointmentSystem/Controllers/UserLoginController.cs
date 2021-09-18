using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorAppointmentSystem.Models;

namespace DoctorAppointmentSystem.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin

        SystemEntities db = new SystemEntities();
        public ViewResult Index()
        {
            //Client cl = new Client()
            //{
            //    email = Email,
            //    password = Password
            //};

            //db.Clients.Find(cl);
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = db.Clients.Where(c => c.email == email && c.password == password).FirstOrDefault();

            if (user == null)
            {
                ViewBag.ErrorMessage = "email or password is not correct!";
                return View("Login");
            }
            Session["userId"] = user.id;
            return RedirectToAction("Index", "Home");
        }
        
        public RedirectToRouteResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "home");
        }
    }
}