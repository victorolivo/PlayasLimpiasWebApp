using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlayasLimpiasWebApp.Models
{
    public class Event
    {
        [Key]
        //Database generated
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name for this event.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a date for this event.")]
        public DateTime Date { get; set; }
        public int NumVolunteersReq { get; set; } = 1;
        [Required(ErrorMessage = "Please choose a location for this event.")]
        public string Location { get; set; }
        public string Image { get; set; } = "";
        public string Description { get; set; } = "";
        public List<User> VolunteersList { get; set; } = null;

    }
}
