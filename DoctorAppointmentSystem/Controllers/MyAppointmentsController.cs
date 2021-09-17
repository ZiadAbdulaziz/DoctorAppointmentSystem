using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorAppointmentSystem.Models;

namespace DoctorAppointmentSystem.Controllers
{
    public class MyAppointmentsController : Controller
    {
        SystemEntities db = new SystemEntities();
        // GET: MyAppointments
        public ActionResult Index()
        {
            List<Appointemt> myAppointments = new List<Appointemt>();
            if (Session["userId"] != null)
            {
                int userId = (int) Session["userId"];
                myAppointments = db.Appointemts.Where(a => a.client_id == userId).ToList();
                var upcoming = myAppointments.Where(a => a.app_date.Date.CompareTo(DateTime.Now.Date) > 0).ToList();
                return View("UserAppointment", myAppointments);
            }
            else if (Session["doctorId"] != null)
            {
                int doctorId = (int)Session["doctorId"];
                myAppointments = db.Appointemts.Where(a => a.doctor_id == doctorId).ToList();
                return View("DoctorAppointment", myAppointments);

            }
            
            return RedirectToAction("Index", "home");
            
        
        }
        public RedirectToRouteResult Cancel(int id)
        {
            Appointemt appointemt = db.Appointemts.Find(id);
            db.Appointemts.Remove(appointemt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}