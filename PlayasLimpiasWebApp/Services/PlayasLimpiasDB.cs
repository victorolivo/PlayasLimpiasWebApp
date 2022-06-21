using PlayasLimpiasWebApp.Models;
using System.Collections.Generic;

namespace PlayasLimpiasWebApp.Services
{
    public class PlayasLimpiasDB : IData
    {
        public List<Event> Events { get; set; }
        public EventContext _eventContext { get; set; }

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

            if (_eventContext.UserEvents != null)
            {
                foreach (var user_Event in _eventContext.UserEvents)
                {
                    if (user_Event.User == user)
                    {
                        myEvents.Add(_eventContext.Events.Find(user_Event.EventId));
                    }
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
            _eventContext.Remove<Event>(_eventContext.Find<Event>(id));
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
                current.NumVolunteers = @event.NumVolunteers;
                current.Location = @event.Location;

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

        public void VolunteerRelationship(Event @event, User user)
        {
            _eventContext.UserEvents.Add(new User_Event
            {
                Event = @event,
                User = user,
                EventId = @event.Id,
                UserId = user.Id
            });

            _eventContext.SaveChangesAsync();
        }

        //Remove volunteer ralationship entries when an event is deleted; Admin ONLY
        public void RemoveEventRelationships(Event @event)
        {
            foreach (var ue in _eventContext.UserEvents)
            {
                if(ue.EventId == @event.Id)
                    _eventContext.Remove<User_Event>(ue);
            }

            _eventContext.SaveChangesAsync();
        }

        //Checks if the current user has alredy volunteer for the selected event
        //True: User is already a volunteer; Flase: User is not a volunteer
        public bool CheckRelationship(Event @event, User user)
        {
            foreach(var ue in _eventContext.UserEvents)
            {
                if(ue.EventId == @event.Id && ue.UserId == user.Id)
                {
                    return true;
                }
            }

            return false;
        }

        public void Unvolunteer(Event @event, User user)
        {
            foreach (var ue in _eventContext.UserEvents)
            {
                if (ue.EventId == @event.Id && ue.UserId == user.Id)
                {
                    _eventContext.UserEvents.Remove(ue);
                }
            }
            _eventContext.SaveChangesAsync();
        }
    }
}
