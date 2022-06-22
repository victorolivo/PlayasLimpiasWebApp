using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using PlayasLimpiasWebApp.Controllers;
using PlayasLimpiasWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class HomeControllerTest
    {
        //SETUP
        private readonly ILogger<HomeController> _logger;
        IData db;
        HomeController controller;

        [SetUp]
        public void Setup()
        {
            controller = new HomeController(_logger, db);
        }

        //TESTS (No DB or User interacted methods; those are covered in functionality testing)

        [Test]
        public void Index_View()
        {
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void Privacy_View()
        {
            var result = controller.Privacy() as ViewResult;
            Assert.AreEqual("Privacy", result.ViewName);
        }

        [Test]
        public void About_View()
        {
            var result = controller.About() as ViewResult;
            Assert.AreEqual("About", result.ViewName);
        }
    }
}
