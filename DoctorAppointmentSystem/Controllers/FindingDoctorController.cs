using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorAppointmentSystem.Models;

namespace DoctorAppointmentSystem.Controllers
{

    public class FindingDoctorController : Controller
    {
         
        readonly SystemEntities db = new SystemEntities();
        
        public ActionResult Index(string sortOrder, string searchString)
        {
            if (Session["userId"] == null)
            {
                ViewBag.ErrorMessage = "You must login first";
                return View("~/Views/UserLogin/Login.cshtml");
            }
            ViewBag.SortParm = String.IsNullOrEmpty(sortOrder) ? "asc" : "";
            ViewBag.SearchString = "";
            var listOFIds = from i in db.DoctorSchedules
                            select i.doctor_id;
            var doctors = db.Doctors.Where(d => listOFIds.Contains(d.id));

            if(!string.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(d => d.name.Contains(searchString));
                ViewBag.SearchString = searchString;

            }
            switch (sortOrder)
            {
                case "des":
                    doctors = doctors.OrderByDescending(d => d.fees);
                    ViewBag.SortParm = "asc";
                    break;
                case "asc":
                    doctors = doctors.OrderBy(d => d.fees);
                    ViewBag.SortParm = "des";
                    break;

            }
            return View("FindingDoctor", doctors.ToList());
        }
        
    }
}