using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorAppointmentSystem.Models;

namespace DoctorAppointmentSystem.Controllers
{
    public class ReservationController : Controller
    {
        SystemEntities db = new SystemEntities(); 
        // GET: Reservation
        [HttpGet]
        public ActionResult Index(int id)
        {
            List<Tuple<DateTime, string>> possibleDates = new List<Tuple<DateTime, string>>();
            var doctor = db.Doctors.Find(id);
            var schedules = db.DoctorSchedules.Where(s => s.doctor_id == id);

            foreach(DoctorSchedule schedule in schedules.ToList())

            {
                DateTime nextDate = GetNextWeekday(DateTime.Now, ConvertToDayOFWeek(schedule.work_day));
                Boolean isAvailable = IsAvailable(id, nextDate, schedule);
                if(isAvailable)
                {
                    possibleDates.Add(new Tuple<DateTime, string>(nextDate, schedule.work_day));
                }

            }
            ViewBag.PossibleDates = possibleDates;
            return View(doctor);
        }

        public RedirectToRouteResult Book(DateTime date, int doctorId)
        {
            Appointemt app = new Appointemt { app_date = date.Date, client_id = (int)Session["userId"], doctor_id = doctorId};
            db.Appointemts.Add(app);
            return RedirectToAction("Index", "Home");
        }

        public DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {

            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
        public Boolean IsAvailable(int doctorId, DateTime date, DoctorSchedule schedule)
        {
            var records = db.Appointemts.Where(a => a.doctor_id == doctorId && a.app_date == date);
            if (records.ToList().Count >= schedule.no_patients)
                return false;
            return true;
        }
        public DayOfWeek ConvertToDayOFWeek(string day)
        {
            DayOfWeek dayOfWeek = DayOfWeek.Saturday;
            switch(day.ToLower())
            {
                case "sat":
                    dayOfWeek = DayOfWeek.Saturday;
                    break;
                case "sun":
                    dayOfWeek = DayOfWeek.Sunday;
                    break;
                case "mon":
                    dayOfWeek = DayOfWeek.Monday;
                    break;
                case "tue":
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case "wed":
                    dayOfWeek = DayOfWeek.Wednesday;
                    break;
                case "thu":
                    dayOfWeek = DayOfWeek.Thursday;
                    break;
                case "fri":
                    dayOfWeek = DayOfWeek.Friday;
                    break;
            }
            return dayOfWeek;
        }
    }


}