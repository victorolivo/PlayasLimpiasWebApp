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


        //CRUD Operations: Role restricted at the controller

        //Adds new event to the database
        public void AddEvent(Event @event)
        {
            _eventContext.Events.Add(@event);
            _eventContext.SaveChangesAsync();
        }

        //Gets the all the events
        public List<Event> GetAllEvents()
        {
            return new List<Event>(_eventContext.Events);
        }

        //Gets events that the current user is actively volunteering
        public List<Event> GetMyEvents(User user)
        {
            List<Event> myEvents = new List<Event>();
            List<Event> allEvents = GetAllEvents();

            foreach (Event @event in allEvents)
            {
                if (@event.VolunteersList != null && @event.VolunteersList.Contains(user))
                {
                    myEvents.Add(@event);
                }
            }



            return myEvents;
        }

        //Gets a specific event
        public Event GetEventById(int id)
        {
            return _eventContext.Events.Find(id);
        }

        //Removes(Deletes) the selected event
        public void RemoveEvent(int id)
        {
            _eventContext.Remove(_eventContext.Find<Event>(id));
            _eventContext.SaveChangesAsync();
        }

        //Updates the modified event
        public void UpdateEvent(Event @event)
        {
            Event current = _eventContext.Events.Find(@event.Id);

            if (current != null)
            {
                current.Name = @event.Name;
                current.Date = @event.Date;
                current.NumVolunteersReq = @event.NumVolunteersReq;
                current.Location = @event.Location;
                current.VolunteersList = @event.VolunteersList;

                if (@event.Image != null)
                {
                    current.Image = @event.Image;
                }
                else
                {
                    current.Image = "b1.jpeg";
                }

                current.Description = @event.Description;

                _eventContext.SaveChanges();
            }
        }


    }
}
