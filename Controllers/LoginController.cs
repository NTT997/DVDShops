using DVDShops.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DVDShops.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private IUserService userService;
        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("")]
        [Route("~/")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("login");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var account = userService.GetByName(username);
            if (account != null)
            {
                if(string.Equals(password,account.UsersPassword.Trim()))
                {
                    if(account.IsAdmin)
                    {
                        HttpContext.Session.SetString("role","admin");
                    } else
                    {
                        HttpContext.Session.SetString("role", "member");
                    }
                    ViewBag.msg = "valid";
                    return View("login");
                }
                ViewBag.msg = "wrong password";
                return View("login");
            }

            ViewBag.msg = "null account";
            return View("login");
        }
    }
}
