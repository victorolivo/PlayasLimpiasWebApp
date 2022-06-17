using PlayasLimpiasWebApp.Models;
using System.Collections.Generic;

namespace PlayasLimpiasWebApp.Services
{
    //Interface for data service
    public interface IData
    {

        List<Event> Events { get; set; }
        public EventContext _eventContext { get; set; }

        //CRUD Operations
        List<Event> GetAllEvents ();
        List<Event> GetMyEvents(User user);

        Event GetEventById(int id);

        //'event' is a reserved keyword, the @ symbol specifies the word 'event' is being used as a variable name
        void AddEvent(Event @event);
        void RemoveEvent(int id);
        void UpdateEvent(Event @event);

        void VolunteerRelationship(Event @event, User user);

    }
}
