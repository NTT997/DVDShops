using DVDShops.Models;
using DVDShops.Services.Users;
using Microsoft.AspNetCore.Mvc;
using DVDShops.Services.MailService;
using System.Diagnostics;

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
        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("login");
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(User user)
        {
            var result = userService.Login(user.UsersEmail, user.UsersPassword);
            Debug.WriteLine("==========================================");
            Debug.WriteLine(result);
            if (result)
            {
                var getUser = userService.GetByEmail(user.UsersEmail);
                if (getUser.IsAdmin)
                {
                    HttpContext.Session.SetString("role", "admin");
                    HttpContext.Session.SetString("email", $"{user.UsersEmail}");
                    return RedirectToAction("index", "dashboard",new {area = "admin"});
                }
                else
                {
                    HttpContext.Session.SetString("role", "member");
                    HttpContext.Session.SetString("email", $"{user.UsersEmail}");
                }
                TempData["msg"] = "Login Success";
                return RedirectToAction("index", "index");
            }
            TempData["msg"] = "Invalid Email or Password";
            return View("login");
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Register(User user, string confirmPassword)
        {
            if (confirmPassword != user.UsersPassword)
            {
                TempData["msg"] = "Password & Confirm Not Match";
                return View("login", user);
            }
            if (string.IsNullOrWhiteSpace(user.UsersEmail) || string.IsNullOrWhiteSpace(user.UsersPassword))
            {

                TempData["msg"] = "Not All Field Is Filled";
                return View("login", user);
            }

            if (user.UsersPassword.Length < 6 || user.UsersPassword.Length > 12)
            {
                TempData["msg"] = "Password Must Be 6-12 Characters";
                return View("login", user);
            }

            if (userService.GetByEmail(user.UsersEmail) != null)
            {
                TempData["msg"] = "The Email already exists";
                return View("login", user);
            }

            var newUser = new User
            {
                UsersEmail = user.UsersEmail,
                UsersPassword = BCrypt.Net.BCrypt.HashPassword(user.UsersPassword),
                UsersProfileName = user.UsersEmail,
                UsersDateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                UsersAddress = "Default",
                UsersPhone = 1111111111,

                UsersActivated = false,
                IsAdmin = false,
                IsCustomer = false,
                IsCancel = false,
                DeleteStatus = false
            };

            var result = userService.Create(newUser);
            if (!result)
            {
                TempData["msg"] = "Cannot Regist Account";
                return View("login", user);
            }

            var baseUrl = configuration["BaseUrl"];
            mailService.SendMail(newUser, "Activate Your Account", $"Please click this link to activate your account: {baseUrl}/verify?userEmail={newUser.UsersEmail}");

            TempData["msg"] = "We have sent an activation email, please check your register email";
            return RedirectToAction("index", "index");
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "index");
        }

    }
}
