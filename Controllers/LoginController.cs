using DVDShops.Models;
using DVDShops.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DVDShops.Services.MailService;
using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {

        private IUserService userService;
        private IMailService mailService;
        private IConfiguration configuration;

        public LoginController(IUserService userService, IMailService mailService, IConfiguration configuration)
        {

            this.userService = userService;
            this.mailService = mailService;
            this.configuration = configuration;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("feedback")]
        [HttpGet]
        public IActionResult fEEDBACK()
        {
            if (HttpContext.Session.GetString("role") != null)
            {

            }
            return View("login");
        }
        
        [HttpPost]
        public IActionResult Login(User user)
        {
            var result = userService.Login(user.UsersEmail, user.UsersPassword);
            if (result)
            {
                if (user.IsAdmin)
                {
                    HttpContext.Session.SetString("role", "admin");
                }
                else
                {
                    HttpContext.Session.SetString("role", "member");
                }
                TempData["Success"] = "Login Success";
                return RedirectToAction("index", "index");
            }
            TempData["Invalid"] = "Invalid Email or Password";
            return View("login");

        }
        [Route("register")]     
        [HttpPost]
        public IActionResult Register(User user, string userPhone, string dob, string confirmPassword)
        {
            if (confirmPassword != user.UsersPassword)
            {
                TempData["confirmpassword"] = "PassWord is uncorrect";
                return View("login", user);
            }
            if (string.IsNullOrWhiteSpace(user.UsersEmail) || string.IsNullOrWhiteSpace(user.UsersPassword) ||
                string.IsNullOrWhiteSpace(user.UsersProfileName) || string.IsNullOrWhiteSpace(user.UsersAddress))
            {

                TempData["checkmail"] = "Email or password is invalid";
                return View("login", user);
            }

            if (user.UsersPassword.Length < 6 || user.UsersPassword.Length > 12)
            {
                TempData["checkpassword"] = "password is invaid";
                return View("login", user);
            }

            if (userService.GetByEmail(user.UsersEmail) != null)
            {
                TempData["emailexists"] = "The Email already exists";
                return View("login", user);
            }

            var newUser = new User
            {
                UsersEmail = user.UsersEmail,
                UsersPassword = BCrypt.Net.BCrypt.HashPassword(user.UsersPassword),
                UsersProfileName = user.UsersEmail,
                UsersDateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                UsersAddress = user.UsersAddress,
                UsersPhone = 0000000000,

                UsersActivated = false,
                IsAdmin = false,
                IsCustomer = false,
                IsCancel = false,
                DeleteStatus = false
            };

            var result = userService.Create(newUser);
            if (!result)
            {
                return View("login", user);
            }

            var baseUrl = configuration["BaseUrl"];
            mailService.SendMail(newUser, "We have sent an activation email, please check to activate your account", $"{baseUrl}/verify?userEmail={newUser.UsersEmail}");


            return RedirectToAction("view", "user");
        }


    }
}
