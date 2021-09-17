using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorAppointmentSystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if(Session["userId"] != null)
            {
                return View("UserHome");
            }

            if (Session["doctorId"] != null)
            {
                return View("DoctorHome");
            }

            return View();
        }
        public ActionResult ForDoctor()
        {
            
            return View("ForDoctors");
        }
    }
}