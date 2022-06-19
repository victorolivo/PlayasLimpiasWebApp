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
using System.Collections.Generic;

namespace PlayasLimpiasWebApp.Controllers
{
    public class EventController : Controller
    {
        //Service injection - database
        readonly IData db;

        //Required to get the user from Identity
        private readonly UserManager<User> UserManager;

        //Required to obtain the hosting enviroment
        private readonly IWebHostEnvironment _hostingEnv;

        public EventController(IData data, IWebHostEnvironment hostingEnv, UserManager<User> userManager)
        {
            db = data;
            _hostingEnv = hostingEnv;
            UserManager = userManager;
        }

        //Index => All Events (UI)
        public IActionResult Index()
        {
            EventCollectionViewModel ecvm = new EventCollectionViewModel();
            ecvm.EventCollection = db.GetAllEvents();

            if (ecvm.EventCollection.Count == 0)
                ViewBag.Message = "There are no events currentlly";

            //Check current user role
            if (HttpContext.User.IsInRole("Admin"))
                ViewBag.Role = "Admin";
            else
                ViewBag.Role = "User";


            return View(ecvm);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Create(Event @event)
        {
            

            if (ModelState.IsValid) //confirms the form data is valid
            {
                //Save event to database
                //Event needs to be sabe into the database first so that a id (PK) is generated
                db.AddEvent(@event);


                //Image Proccessing - Save uploaded image into wwwroot/images folder

                if(@event.ImageFile == null)
                {
                    //default image
                    @event.Image = "b1.jpeg";
                }
                else
                {
                    string wwwRootPath = _hostingEnv.WebRootPath; //get the path for image storage in wwwroot mathching/according to hosting enviroment; physical path

                    //Image file info
                    string imageName = $"imageEventID({@event.Id})"; //custom name for the uploaded image
                    string imageExt = Path.GetExtension(@event.ImageFile.FileName); //get image extension

                    //Give the image a unique name to avoid data conflicts and assing it to the Event.Image property
                    @event.Image = imageName = $"{imageName}{imageExt}";

                    //Final image storage path string
                    string path = Path.Combine($"{wwwRootPath}/images/", imageName);

                    //Save the uploaded image
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await @event.ImageFile.CopyToAsync(stream);
                    }
                }

                //User that creates event is the first volunteer by default
                @event.NumVolunteers = 1;
                var userName = HttpContext.User.Identity.Name;
                User currentUser = await UserManager.FindByNameAsync(userName);
                db.VolunteerRelationship(@event, currentUser);

                db.UpdateEvent(@event);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Event @event = db.GetEventById(id);
            return View(@event);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]//Confirmation Page
        public IActionResult Delete(int id)
        {
            Event @event = db.GetEventById(id);

            if (@event == null)
                return NotFound();


            return View(@event);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(Event @event)
        {
            //Get event image name
            string eventImageName = (db.GetEventById(@event.Id)).Image;

            //Delete event image from wwwroot
            var path = Path.Combine(_hostingEnv.WebRootPath, "images", eventImageName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }


            db.RemoveEventRelationships(@event);
            db.RemoveEvent(@event.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Event @event = db.GetEventById(id);

            if (@event == null)
                return NotFound();

            //Check if the user is already volunteering for this event
            var userName = HttpContext.User.Identity.Name;
            User currentUser = await UserManager.FindByNameAsync(userName);
            if (db.CheckRelationship(@event, currentUser))
            {
                ViewBag.Message = "Exists";
            }

            //Check current user role
            if (HttpContext.User.IsInRole("Admin"))
                ViewBag.Role = "Admin";
            else
                ViewBag.Role = "User";

            return View(@event);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Volunteer(int id)
        {
            var userName = HttpContext.User.Identity.Name;
            User currentUser = await UserManager.FindByNameAsync(userName);

            Event @event = db.GetEventById(id);

            //Check if the user is already volunteering for this event
            if (db.CheckRelationship(@event, currentUser))
            {
                ViewBag.Message = "Exists";
            }
            else
            {
                db.VolunteerRelationship(@event, currentUser);
                ViewBag.Message = "Success";
                @event.NumVolunteers++;
                db.UpdateEvent(@event);
            }
            
            return View("Details", @event);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Unvolunteer(int id)
        {
            var userName = HttpContext.User.Identity.Name;
            User currentUser = await UserManager.FindByNameAsync(userName);

            Event @event = db.GetEventById(id);

            db.Unvolunteer(@event, currentUser);
            @event.NumVolunteers--;
            db.UpdateEvent(@event);

            return RedirectToAction("MyEvents");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyEvents()
        {
            EventCollectionViewModel ecvm = new EventCollectionViewModel();

            //Get user
            var userName = HttpContext.User.Identity.Name;
            User currentUser = await UserManager.FindByNameAsync(userName);

            ecvm.EventCollection = db.GetMyEvents(currentUser);

            if(ecvm.EventCollection.Count == 0)
                ViewBag.Message = "You do not have any events, check out all the ongoing events below";
            return View(ecvm);
        }
    }
}
