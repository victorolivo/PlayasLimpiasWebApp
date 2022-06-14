using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlayasLimpiasWebApp.Models;
using PlayasLimpiasWebApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PlayasLimpiasWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IData db;

        public HomeController(ILogger<HomeController> logger, IData _data)
        {
            _logger = logger;
            db = _data;
        }

        public IActionResult Index()
        {
            int highlightedEventId = 1;
            Event @event = db.GetEventById(highlightedEventId);
            return View(@event);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
