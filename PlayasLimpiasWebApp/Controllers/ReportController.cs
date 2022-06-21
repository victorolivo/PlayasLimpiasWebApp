using Microsoft.AspNetCore.Mvc;
using PlayasLimpiasWebApp.Models;
using PlayasLimpiasWebApp.Services;
using PlayasLimpiasWebApp.ViewModels;
using System;
using System.Collections.Generic;

namespace PlayasLimpiasWebApp.Controllers
{
    public class ReportController : Controller
    {
        IData db;

        public ReportController(IData data)
        {
            db = data;
        }

        public IActionResult ActivityReport()
        {
            ActivityCollectionViewModel report = new ActivityCollectionViewModel();
            report.ActivityCollection = db.GetAllActivity();

            return View(report);
        }

        public IActionResult VolunteeringReport()
        {
            int activeVolunteers = 0;
            List<Event> lessThan50percentage = new List<Event>();

            foreach(var e in db.GetAllEvents())
            {
                activeVolunteers += e.NumVolunteers;

                if(e.NumVolunteers < (e.NumVolunteersReq/2))
                    lessThan50percentage.Add(e);
            }

            VolunteeringReportViewModel report = new VolunteeringReportViewModel
            {
                timeStamp = DateTime.Now.ToString("f"),
                NumActiveUsers = db.GetAllUsers().Count,
                NumActiveEvents = db.GetAllEvents().Count,
                NumActiveVolunteers = activeVolunteers,
                LessThan50PerOfVolunteers = lessThan50percentage,
                VolunteeringPercentage = (int)((activeVolunteers/db.GetAllUsers().Count)*100)

            };


            return View(report);
        }
    }
}
