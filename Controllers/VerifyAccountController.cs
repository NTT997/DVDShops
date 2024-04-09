using DVDShops.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Controllers
{
    [Route("verify")]
    public class VerifyAccountController : Controller
    {
        private IUserService userService;
        public VerifyAccountController(IUserService userService) { this.userService = userService; }


        [Route("")]
        [HttpGet]
        public IActionResult Verify(string userEmail)
        {
            var user = userService.GetByEmail(userEmail);

            if (user != null)
            {
                user.UsersActivated = true;
                userService.Update(user);
            }
            TempData["msg"] = "Activated Please Login Again!";
            return RedirectToAction("login", "login");
        }
    }
}
