using PlayasLimpiasWebApp.Models;
using System.Collections.Generic;

namespace PlayasLimpiasWebApp.Services
{
    //Interface for data service
    public interface IData
    {

        List<Event> Events { get; set; }

        //CRUD Operations
        List<Event> GetAllEvents ();
        List<Event> GetMyEvents(int userId);

        //'event' is a reserved keyword, the @ symbol specifies the word 'event' is being used as a variable name
        void AddEvent(Event @event);
        void RemoveEvent(Event @event);
        void UpdateEvent(Event @event);

    }
}
