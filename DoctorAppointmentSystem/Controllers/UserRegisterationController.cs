using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorAppointmentSystem.Models;

namespace DoctorAppointmentSystem.Controllers
{
    public class UserRegisterationController : Controller
    {
        // GET: UserRegisteration
        SystemEntities db = new SystemEntities();

        public ViewResult Index()
        {
            //List<Client> depts = db.Clients.ToList<Client>();

            //return View(depts);
            return View("Registeration1");
        }
        //public ActionResult Registeration1()
        //{
        //    return View();
        //}

        [HttpPost]
        public RedirectToRouteResult Registeration(Client c)
        {
            Session["userId"] = c.id;
            db.Clients.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}