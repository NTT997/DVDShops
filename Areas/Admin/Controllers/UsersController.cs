using DVDShops.Models;
using DVDShops.Services.MailService;
using DVDShops.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/user")]
    public class UsersController : Controller
    {
        private IUserService userService;
        private IMailService mailService;
        private IConfiguration configuration;
        public UsersController(IUserService userService, IMailService mailService, IConfiguration configuration)
        {
            this.userService = userService;
            this.mailService = mailService;
            this.configuration = configuration;
        }

        private void SetTempData(bool status, string title, string array)
        {
            TempData["status"] = status.ToString();
            TempData["title"] = title;
            TempData["message"] = array;
        }

        [Route("")]
        [Route("view")]
        public IActionResult UserView()
        {
            return View("UserView");
        }

        [Route("addUser")]
        [HttpGet]
        public IActionResult UserAdd()
        {
            return View("UserAdd");
        }

        [Route("addUser")]
        [HttpPost]
        public IActionResult UserAdd(User user, string userPhone, string dob, string confirmPassword)
        {
            if (confirmPassword != user.UsersPassword)
            {
                SetTempData(false, "Create User Failed!", "Confirm Password & Password Not The Same!");
                TempData["dob"] = dob;
                return View("UserAdd", user);
            }

            if (string.IsNullOrEmpty(dob))
            {
                SetTempData(false, "Create User Failed!", "Invalid DoB!");
                TempData["dob"] = dob;
                return View("UserAdd", user);
            }
            else
            {
                var parseDate = DateOnly.Parse(dob);
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                var age = today.Year - parseDate.Year;
                if (parseDate.AddYears(age) > today)
                {
                    age--;
                }

                if (age < 18)
                {
                    SetTempData(false, "Create User Failed!", "User Must be At Least 18!");
                    TempData["dob"] = dob;
                    return View("UserAdd", user);
                }
            }

            long parsedPhone;
            if (userPhone.Length == 12)
            {
                if (long.TryParse(userPhone.Replace(" ", ""), out parsedPhone))
                {
                    if (parsedPhone < 999999999)
                    {
                        SetTempData(false, "Create User Failed!", "Invalid Phone Number!");
                        TempData["dob"] = dob;
                        return View("UserAdd", user);
                    }
                }
                else
                {
                    SetTempData(false, "Create User Failed!", "Invalid Phone Number!");
                    TempData["dob"] = dob;
                    return View("UserAdd", user);
                }
            }
            else
            {
                SetTempData(false, "Create User Failed!", "Invalid Phone Number!");
                TempData["dob"] = dob;
                return View("UserAdd", user);
            }

            if (string.IsNullOrWhiteSpace(user.UsersEmail) || string.IsNullOrWhiteSpace(user.UsersPassword) ||
                string.IsNullOrWhiteSpace(user.UsersProfileName) || string.IsNullOrWhiteSpace(user.UsersAddress))
            {
                SetTempData(false, "Create User Failed!", "Some Input Field Is White Space Only!");
                TempData["dob"] = dob;
                return View("UserAdd", user);
            }

            if (user.UsersPassword.Length < 6 || user.UsersPassword.Length > 12)
            {
                SetTempData(false, "Create User Failed!", "Invalid Password!");
                TempData["dob"] = dob;
                return View("UserAdd", user);
            }

            if (userService.GetByEmail(user.UsersEmail) != null)
            {
                SetTempData(false, "Create User Failed!", "This Email Is Already in Used!");
                TempData["dob"] = dob;
                return View("UserAdd", user);
            }

            var newUser = new User
            {
                UsersEmail = user.UsersEmail,
                UsersPassword = BCrypt.Net.BCrypt.HashPassword(user.UsersPassword),
                UsersProfileName = user.UsersProfileName,
                UsersDateOfBirth = DateOnly.Parse(dob),
                UsersAddress = user.UsersAddress,
                UsersPhone = parsedPhone,

                UsersActivated = false,
                IsAdmin = user.IsAdmin,
                IsCustomer = false,
                IsCancel = false,
                DeleteStatus = false
            };

            var result = userService.Create(newUser);
            if (!result)
            {
                SetTempData(false, "Create User Failed!", "Cannot Create This User!");
                TempData["dob"] = dob;
                return View("UserAdd", user);
            }

            var baseUrl = configuration["BaseUrl"];
            mailService.SendMail(newUser, "Your Account Have Been Create! Click this link to ACTIVATE your account", $"{baseUrl}/verify?userEmail={newUser.UsersEmail}");

            SetTempData(true, "Create User Success!", $"{newUser.UsersEmail} Just Added!");
            return RedirectToAction("view", "user");
        }

        [Route("detailUser")]
        [HttpGet]
        public IActionResult MemberDetail(int userId)
        {
            var user = userService.GetById(userId);
            TempData["dob"] = $"{user.UsersDateOfBirth:yyyy-MM-dd}";
            return View("UserDetail", user);
        }

        [Route("editUser")]
        [HttpPost]
        public IActionResult UserEdit(User user, string userPhone, string dob, string newEmail)
        {
            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                if (newEmail.Trim() == user.UsersEmail)
                {
                    SetTempData(false, "Update User Failed!", "New Email is The Same!");
                    TempData["dob"] = dob;
                    return View("UserDetail", user);
                }

                if (!string.IsNullOrEmpty(newEmail))
                {
                    user.UsersEmail = newEmail;
                }
            }

            if (string.IsNullOrEmpty(dob))
            {
                SetTempData(false, "Update User Failed!", "Invalid DoB!");
                TempData["dob"] = dob;
                return View("UserDetail", user);
            }
            else
            {
                var parseDate = DateOnly.Parse(dob);
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                var age = DateOnly.FromDateTime(DateTime.Today).Year - DateOnly.Parse(dob).Year;
                if (parseDate.AddYears(age) > today)
                {
                    age--;
                }

                if (age < 18)
                {
                    SetTempData(false, "Update User Failed!", "User Must be At Least 18!");
                    TempData["dob"] = dob;
                    return View("UserDetail", user);
                }
            }

            long parsedPhone;
            if (userPhone.Length == 12)
            {
                if (long.TryParse(userPhone.Replace(" ", ""), out parsedPhone))
                {
                    if (parsedPhone < 999999999)
                    {
                        SetTempData(false, "Create User Failed!", "Invalid Phone Number!");
                        TempData["dob"] = dob;
                        return View("UserAdd", user);
                    }
                }
                else
                {
                    SetTempData(false, "Create User Failed!", "Invalid Phone Number!");
                    TempData["dob"] = dob;
                    return View("UserAdd", user);
                }
            }
            else
            {
                SetTempData(false, "Create User Failed!", "Invalid Phone Number!");
                TempData["dob"] = dob;
                return View("UserAdd", user);
            }

            user.UsersPhone = parsedPhone;
            user.UsersDateOfBirth = DateOnly.Parse(dob);
            var result = userService.Update(user);

            if (!result)
            {
                SetTempData(false, "Update User Failed!", "Cannot Update Now!");
                TempData["dob"] = dob;
                return View("UserAdd", user);
            }
            SetTempData(true, "Update User Success!", $"{user.UsersEmail} Updated!");
            return RedirectToAction("view", "user");
        }

        [Route("deleteUser")]
        [HttpGet]
        public IActionResult DeleteUser(int userId)
        {
            var user = userService.GetById(userId);
            user.DeleteStatus = !user.DeleteStatus;

            if (userService.Update(user))
            {
                if (user.DeleteStatus)
                {
                    SetTempData(true, "Delete User Success!", $"{user.UsersEmail} Deleted!");
                }
                else
                {
                    SetTempData(true, "Recover User Success!", $"{user.UsersEmail} Recoverd!");
                }
            }
            else
            {
                SetTempData(false, "Delete User Failed!", "Something Wrong!");
            }

            return RedirectToAction("view", "user");
        }

        [Route("resetPassword")]
        [HttpGet]
        public IActionResult ResetPassword(int userId)
        {
            var user = userService.GetById(userId);
            var newPass = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12);
            user.UsersPassword = BCrypt.Net.BCrypt.HashPassword(newPass);

            if (userService.Update(user))
            {
                mailService.SendMail(user, "Reset Password", $"Your Password Has Been Reset New Password: {newPass}");
            }

            SetTempData(true, "Reset Password Success!", $"Please Check Email {user.UsersEmail}");
            return RedirectToAction("detailUser", "user", new { userId = userId });
        }

        [Route("changePassword")]
        [HttpGet]
        public IActionResult ChangePassword(int userId)
        {
            var user = userService.GetById(userId);
            return View("ChangePassword", user);
        }

        [Route("changePassword")]
        [HttpPost]
        public IActionResult ChangePassword(User user, string oldPassword, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                SetTempData(false, "Change Password Failed!", "Some Input Field Is White Space Only!");
                return View("ChangePassword", user);
            }

            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.UsersPassword))
            {
                SetTempData(false, "Change Password Failed!", "Old Password Not Correct!");
                return View("ChangePassword", user);
            }

            if (newPassword.Trim() == oldPassword.Trim())
            {
                SetTempData(false, "Change Password Failed!", "New Pass Cannot Be The Same As Old Password!");
                return View("ChangePassword", user);
            }

            if (newPassword.Trim() != confirmPassword.Trim())
            {
                SetTempData(false, "Change Password Failed!", "Confirm Password Not The Same As New Password!");
                return View("ChangePassword", user);
            }

            user.UsersPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            userService.Update(user);
            SetTempData(true, "Change Password Success!", $"{user.UsersEmail}'s Password Updated!");
            return RedirectToAction("detailUser", "user", new { userId = user.UsersId });
        }
    }
}
