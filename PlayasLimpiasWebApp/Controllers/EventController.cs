using Microsoft.AspNetCore.Mvc;
using PlayasLimpiasWebApp.Models;
using PlayasLimpiasWebApp.Services;

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

        //In review
        public IActionResult MyEvents()
        {
            EventCollectionViewModel ecvm = new EventCollectionViewModel();
            ecvm.EventCollection = db.GetMyEvents((User)this.User.Identity);
            return View(ecvm);
        }
    }
}
