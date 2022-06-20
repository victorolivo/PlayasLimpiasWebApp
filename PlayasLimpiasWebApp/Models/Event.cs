using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayasLimpiasWebApp.Models
{
    public class Event
    {
        [Key]
        //Database generated Id
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name for this event.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a date for this event.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please enter an estimated of required volunteers.")]
        [Display(Name = "Number of Volunteers")]
        public int NumVolunteersReq { get; set; }
        public int NumVolunteers { get; set; }
        [Required(ErrorMessage = "Please choose a location for this event.")]
        public string Location { get; set; }

        [Display(Name = "Image Name")]
        public string Image { get; set; }

        [NotMapped] //images will not be included in db; they will be stored in wwwroot
        [Display(Name = "Upload an image (optional)")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Description (optional)")]
        public string Description { get; set; }

        public List<User_Event> UserEvents { get; set; }

    }
}
