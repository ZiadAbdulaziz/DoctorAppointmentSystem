using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using DoctorAppointmentSystem.Models;

namespace DoctorAppointmentSystem.Controllers
{
    public class DoctorRegisterationController : Controller
    {
        SystemEntities db = new SystemEntities();


        // GET: DoctorRegisteration

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult Index(string name, int age, string gender, string speciality, string Address, string email, string password, double fees, string bio)
        {
            Doctor doc = new Doctor() { name = name, age = age, gender = gender, speciality = speciality, Address = Address, email = email, password = password, fees = fees, bio = bio };

            db.Doctors.Add(doc);
            db.SaveChanges();
            Session["doctorId"] = doc.id;
            return RedirectToAction("Schedule");

        }

        [HttpGet]
        public ActionResult Schedule()
        {    
            var doc = db.Doctors.Find(Session["doctorId"]);
            List<String> availableDays = new List<string>() { "SAT", "SUN", "MON", "TUE", "WED", "THU", "FRI" };
            
            var existedDays = db.DoctorSchedules.Where(d => d.doctor_id == doc.id).ToList();
            foreach (var day in existedDays)
            {
                availableDays.Remove(day.work_day.ToUpper());
            }
            ViewBag.AvailableDays = availableDays;
            return View(doc);
        }
        [HttpPost]
        public RedirectToRouteResult Schedule( int noPatients, string workDay)
        {

            DoctorSchedule sch = new DoctorSchedule() { doctor_id = (int)Session["doctorId"], work_day = workDay, no_patients = noPatients};
            db.DoctorSchedules.Add(sch);
            db.SaveChanges();
            return RedirectToAction("Schedule");
        }

        
        
    }
}