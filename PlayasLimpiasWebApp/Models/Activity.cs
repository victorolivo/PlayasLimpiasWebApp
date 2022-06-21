using System;
using System.ComponentModel.DataAnnotations;

namespace PlayasLimpiasWebApp.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string ActionBy { get; set; }
        public string AffectedEvent { get; set; }
        public DateTime ActionTimeStamp { get; set; }
    }
}
