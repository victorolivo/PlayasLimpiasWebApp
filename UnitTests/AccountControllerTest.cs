using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PlayasLimpiasWebApp.Controllers;
using PlayasLimpiasWebApp.Models;
using PlayasLimpiasWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class AccountControllerTest
    {
        //SETUP
        readonly IData db;
        private SignInManager<User> SignInManager;
        private UserManager<User> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        AccountController controller;

        [SetUp]
        public void Setup()
        {
            controller = new AccountController(db, SignInManager, UserManager, RoleManager);
        }

        //TESTS (No DB or User interacted methods; those are covered in functionality testing)

        [Test]
        public void Registration_View()
        {
            var result = controller.Register() as ViewResult;
            Assert.AreEqual("Register", result.ViewName);
        }
    }
}
