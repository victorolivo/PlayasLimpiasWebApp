using Microsoft.AspNetCore.Mvc;

namespace PlayasLimpiasWebApp.Controllers
{
    public class EventController : Controller
    {
        //Index => All Events (UI)
        public IActionResult Index()
        {
            return View();
        }
    }
}
