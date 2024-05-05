using Microsoft.AspNetCore.Mvc;
using Task_4.Interfaces;
using Task_4.Mocks;
using Task4DataBase;
using Task4DataBase.Models;

namespace Task_4.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUser _users;
        public AuthController(IUser users) { _users = users; }
        public IActionResult Authorization(string? Message)
        {
            ViewBag.Message = Message;
            return View();
        }

        [HttpPost]
        [Route("/AuthenticateUser")]
        [ActionName("AuthenticateUser")]
        public IActionResult AuthenticateUser(IFormCollection userdata)
        {
            User user = new User();
            user.Email = userdata["Email"];
            user.Password = userdata["Password"].ToString().CalculateHash();
            if (_users.IsUserRegistered(user))
            {
                var checkeduser = _users.GetById(_users.GetUserId(user));
                if (checkeduser != null) 
                {
                    if (checkeduser.Password == user.Password)
                    {
                        checkeduser.LastLoginDate = DateTime.UtcNow;
                        checkeduser.IsBlocked = false;
                        _users.UpdateUser(checkeduser);
                        HttpContext.Session.SetInt32("userid", checkeduser.Id);
                        return RedirectToAction("Users", "Home");
                    }
                }
            }
            return RedirectToAction("Authorization", "Auth", new { Message = "Wrong password or email" });
        }
    }
}
