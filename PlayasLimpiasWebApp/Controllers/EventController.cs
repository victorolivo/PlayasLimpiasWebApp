using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PlayasLimpiasWebApp.Models;
using PlayasLimpiasWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Threading.Tasks;

namespace PlayasLimpiasWebApp.Controllers
{
    public class EventController : Controller
    {
        //Service injection - database
        IData db;

        //Required to obtain the hosting enviroment
        private readonly IWebHostEnvironment _hostingEnv;

        public EventController(IData data, IWebHostEnvironment hostingEnv)
        {
            db = data;
            _hostingEnv = hostingEnv;
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
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid) //confirms the form data is valid
            {
                //Image Proccessing - Save uploaded image into wwwroot/images folder

                string wwwRootPath = _hostingEnv.WebRootPath; //get the path for image storage in wwwroot mathching/according to hosting enviroment; physical path

                //Image file info
                string imageName = $"imageEventID({@event.Id})"; //custom name for the uploaded image
                string imageExt = Path.GetExtension(@event.ImageFile.FileName); //get image extension

                //Give the image a unique name to avoid data conflicts and assing it to the Event.Image property
                @event.Image = imageName = $"{imageName}{imageExt}";

                //Final image storage path string
                string path = Path.Combine($"{wwwRootPath}/images/", imageName);

                //Save the uploaded image
                using(var stream = new FileStream(path, FileMode.Create))
                {
                    await @event.ImageFile.CopyToAsync(stream);
                }

                //Save event to database
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
