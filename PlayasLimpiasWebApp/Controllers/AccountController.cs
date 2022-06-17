using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PlayasLimpiasWebApp.Models;
using System.Threading.Tasks;
using PlayasLimpiasWebApp.ViewModels;

namespace PlayasLimpiasWebApp.Controllers
{
    public class AccountController : Controller
    {
        //Account managemnet properties
        private SignInManager<User> SignInManager;
        private UserManager<User> UserManager;
        private RoleManager<IdentityRole> RoleManager;


        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        //User request login view
        [HttpGet]
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //User attempts to login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)//login form is completed properly; Restrictions established on LoginViewModel
            {
                var result = await SignInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Failed login");
            return View();
        }

        //User request register view
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //User attempts registration
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create new registered user
                User newUser = new User()
                {
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    Email = registerViewModel.Email,
                    PhoneNumber = registerViewModel.PhoneNumber,
                    UserName = registerViewModel.UserName
                };

                //Registration
                var result = await UserManager.CreateAsync(newUser, registerViewModel.Password);

                //Role assignment
                if (result.Succeeded)
                {
                    var addedUser = await UserManager.FindByNameAsync(registerViewModel.UserName);
                    if (addedUser.UserName == "admin")
                    {
                        await UserManager.AddToRoleAsync(addedUser, "ADMIN");
                        await UserManager.AddToRoleAsync(addedUser, "USER");
                    }
                    else
                    {
                        await UserManager.AddToRoleAsync(addedUser, "USER");
                    }

                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            ModelState.AddModelError("", "Registration Failed");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
