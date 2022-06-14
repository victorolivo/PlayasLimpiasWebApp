using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PlayasLimpiasWebApp.Models;
using PlayasLimpiasWebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace PlayasLimpiasWebApp.Controllers
{
    public class EventController : Controller
    {
        //Service injection - database
        IData db;

        public EventController(IData data)
        {
            db = data;
        }

        //Index => All Events (UI)
        public IActionResult Index()
        {
            EventCollectionViewModel ecvm = new EventCollectionViewModel();
            ecvm.EventCollection = db.GetAllEvents();
            return View(ecvm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Event @event)
        {
            if (ModelState.IsValid) //confirms the form data is valid
            {
                db.AddEvent(@event);
                ViewBag.Message = "Event added successfully!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Event @event = db.GetEventById(id);
            return View(@event);
        }

        [HttpPost]
        public IActionResult Edit(Event @event)
        {
            if (ModelState.IsValid) //confirms the form data is valid
            {
                db.UpdateEvent(@event);
                ViewBag.Message = "Event has been updated successfully!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            db.RemoveEvent(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Event @event = db.GetEventById(id);

            if (@event == null)
                return NotFound();


            return View(@event);
        }

        [Authorize(Roles = "USER")]
        public IActionResult Voluteer(int eventId)
        {
            var userName = this.User.Identity.Name;
            User currentUser = db.GetUser(userName);
            Event @event = db.GetEventById(eventId);

            @event.VolunteersList.Add(currentUser);
            db.UpdateEvent(@event);

            return View();
        }

        //In review
        public IActionResult MyEvents()
        {
            EventCollectionViewModel ecvm = new EventCollectionViewModel();
            var userName = this.User.Identity.Name;
            User currentUser = db.GetUser(userName);
            ecvm.EventCollection = db.GetMyEvents(currentUser);
            return View(ecvm);
        }
    }
}
