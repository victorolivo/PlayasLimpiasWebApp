using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PlayasLimpiasWebApp.Controllers;
using PlayasLimpiasWebApp.Models;
using PlayasLimpiasWebApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTests
{

    public class EventControllerTest
    {
        //SETUP

        IData data;
        IWebHostEnvironment hostEnvironment;
        UserManager<User> userManager;
        EventController controller;
        EventCollectionViewModel viewModel;

        [SetUp]
        public void Setup()
        {
            controller = new EventController(data, hostEnvironment, userManager);
            viewModel = new EventCollectionViewModel();
        }


        //TESTS (No DB or User interacted methods; those are covered in functionality testing)

        [Test]
        public void Create_View()
        {
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

        [Test]
        public void SearchFunction()
        {
            EventCollectionViewModel eventCollection = new EventCollectionViewModel();
            eventCollection.EventCollection = new List<Event>();

            Event a = new Event
            {
                Id = 1,
                Name = "Playa Sucia Event",
                Date = DateTime.Now,
                NumVolunteersReq = 10,
                Location = "Poza del Obispo, Arecibo",
                Description = ""
            };

            Event b = new Event
            {
                Id = 2,
                Name = "Escambron Rescue",
                Date = DateTime.Now,
                NumVolunteersReq = 41,
                Location = "Playa El Escambrón, San Juan",
                Description = ""
            };

            eventCollection.EventCollection.Add(a);
            eventCollection.EventCollection.Add(b);
            string search = "sucia";

            var result = controller.SearchResults(eventCollection, search);

            Assert.Contains(a, result);
        }
    }
}
