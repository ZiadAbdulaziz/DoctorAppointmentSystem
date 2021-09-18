using DoctorAppointmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorAppointmentSystem.Models
{
    public class HomeController : Controller
    {
        // GET: Home
        SystemEntities db = new SystemEntities();
        public ActionResult Index()
        {
            if(Session["userId"] != null)
            {
                var c = db.Clients.Find((int)Session["userId"]);
                return View("UserHome",c);
            }

            if (Session["doctorId"] != null)
            {
                var d = db.Doctors.Find((int)Session["doctorId"]);
                return View("DoctorHome",d);
            }

            return View();
        }
        public ActionResult ForDoctor()
        {
            
            return View("ForDoctors");
        }
    }
}