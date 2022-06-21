using PlayasLimpiasWebApp.Models;
using System;
using System.Collections.Generic;

namespace PlayasLimpiasWebApp.ViewModels
{
    public class VolunteeringReportViewModel
    {
        public string Title { get; set; } = "Event Volunteering Activity Report";
        public string timeStamp { get; set; }

        public int NumActiveUsers { get; set; }
        public int NumActiveEvents { get; set; }
        public int NumActiveVolunteers { get; set; }
        public int VolunteeringPercentage { get; set; }
        public List<Event> LessThan50PerOfVolunteers { get; set; }

    }
}
