using DVDShops.Models;
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
        [HttpGet]
        public IActionResult Login()
        {
            return View("login");
        }

        [Route("feedback")]
        [HttpGet]
        public IActionResult fEEDBACK()
        {
            if(HttpContext.Session.GetString("role") != null)
            {

            }
            return View("login");
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var account = userService.GetByEmail(email);
            if (account != null)
            {
                if (string.Equals(password, account.UsersPassword.Trim()))
                {
                    if (account.IsAdmin)
                    {
                        HttpContext.Session.SetString("role", "admin");
                    }
                    else
                    {
                        HttpContext.Session.SetString("role", "member");
                    }

                    /*
                     * neu co the hay viet 1 cai modal thong bao cho user dang nhap => neu role == admin
                     * hien ra 1 modal confirm ban dang nhap role la admin, ban co muon vao trang admin hay ko
                     * co => return RedirectToAction("admin", "admin", new{ area="admin" });
                     * khong => return RedirectToAction("index", "index");
                     * ======
                     * neu lam modal thi sua doan return ben duoi thang return view login khong lam duoc modal thi bo qua comment nay
                    */
                    return RedirectToAction("index", "index");
                }

                // viet lai login cho nay khi user dang nhap sai password, khi tra ve trang login hien thong bao sai password => thong bao hien ra o trang html
                ViewBag.msg = "wrong password";
                return View("login");
            }

            // viet lai login cho nay khi username ko ton tai, khi tra ve trang login hien thong bao ko ton tai username => thong bao hien ra o trang html
            ViewBag.msg = "null account";
            return View("login");
        }


        //uncomment doan code duoi lam tiep
        // viet logic xu ly register
        // su dung @model, xem lai clip, ss23
        // viet lai code regis cac truong thong tin theo yeu cau cua database
        // sau khi regis thang cong tra lai trang login su dung redirecttoaction
        // nho rang buoc dk input, check ben trong database khi regis can nhung du lieu gi

        //[HttpPost]
        //[Route("register")]
        //public IActionResult Register( dua du lieu can vao day )
        //{
        //    return View();
        //}
    }
}
