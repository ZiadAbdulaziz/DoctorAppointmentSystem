using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorAppointmentSystem.Models;

namespace DoctorAppointmentSystem.Controllers
{
    public class MyScheduleController : Controller
    {
        SystemEntities db = new SystemEntities();
        // GET: MySchedule
        public ActionResult Index()
        {
            if (Session["doctorId"] == null)
            {
                return RedirectToAction("Index", "DoctorLogin", new { errorMessage = "You must login first" });
            }
            int doctorId = (int)Session["doctorId"];
            var schedules = db.DoctorSchedules.Where(d => d.doctor_id == doctorId).ToList();
            return View("MySchedule", schedules);
            
        }
        
        [HttpGet]
        public ActionResult EditSchedule(string workDay)
        {
            if (Session["doctorId"] == null)
            {
                return RedirectToAction("Index", "DoctorLogin", new { errorMessage = "You must login first" });
            }
            int doctorId = (int)Session["doctorId"];
            DoctorSchedule schedule = db.DoctorSchedules.Where(d => d.doctor_id == doctorId && d.work_day == workDay).FirstOrDefault();
            return View("EditSchedule", schedule);
        }
        [HttpPost]
        public ActionResult UpdateSchedule(string workDay, int noPatients)
        {
            if (Session["doctorId"] == null)
            {
                return RedirectToAction("Index", "DoctorLogin", new { errorMessage = "You must login first" });
            }
            int doctorId = (int)Session["doctorId"];
            var schedule = db.DoctorSchedules.Where(d => d.doctor_id == doctorId && d.work_day == workDay).FirstOrDefault();
            schedule.no_patients = noPatients;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}