using System.ComponentModel.DataAnnotations;

namespace PlayasLimpiasWebApp.Models
{
    public class User_Event//Join table neccessary for our many-to-many relationship (a user can volunteer for many events, an event can have many volunteers(users))
    {
        [Key]//Database generated Id
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }
    }
}
