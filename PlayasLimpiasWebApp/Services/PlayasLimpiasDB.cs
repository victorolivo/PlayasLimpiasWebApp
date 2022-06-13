using PlayasLimpiasWebApp.Models;
using System.Collections.Generic;

namespace PlayasLimpiasWebApp.Services
{
    public class PlayasLimpiasDB : IData
    {
        public List<Event> Events { get; set; }

        private EventContext _eventContext;

        //Constructor: EntityFramework injection
        public PlayasLimpiasDB(EventContext eventContext)
        {
            _eventContext = eventContext;
        }

        public void AddEvent(Event @event)
        {
            _eventContext.Events.Add(@event);
            _eventContext.SaveChangesAsync();
        }

        //Get the Events table and returns it as a List collection
        public List<Event> GetAllEvents()
        {
            return new List<Event>(_eventContext.Events);
        }

        public List<Event> GetMyEvents(int userId)
        {
            List<Event> myEvents = new List<Event>();
            
            foreach(Event @event in _eventContext.Events)
            {
                if (@event.VolunteersIdList.Contains(userId))
                {
                    myEvents.Add(@event);
                }
            }

            return myEvents;
        }

        public void RemoveEvent(Event @event)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateEvent(Event @event)
        {
            throw new System.NotImplementedException();
        }
    }
}
