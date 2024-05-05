using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Task_4.Interfaces;
using Task4DataBase.Context;
using Task4DataBase.Models;

namespace Task_4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUser _users;
        public HomeController(ILogger<HomeController> logger, IUser users)
        {
            _logger = logger;
            _users = users;
        }

        public IActionResult Users()
        {
            User currentuser = new User();
            if (HttpContext.Session.GetInt32("userid") != 0 && HttpContext.Session.Keys.Contains("userid"))
            {
                ViewBag.Header = "~/Views/Shared/_SignedLayout.cshtml";
            }
            else ViewBag.Header = "~/Views/Shared/_Layout.cshtml";
            if (HttpContext.Session.Keys.Count() != 0)
            {
                currentuser = _users.GetById(HttpContext.Session.GetInt32("userid").Value);
            }
            if (currentuser.IsBlocked || currentuser.Id == 0)
            {
                return RedirectToAction("Authorization", "Auth");
            }
            ViewBag.Users = _users.GetAll();
            ViewBag.CurrentUser = currentuser;
            return View("Users");
        }

        [HttpPost]
        public IActionResult delete([FromBody] List<int> userIds)
        {
            User currentuser = new User();
            if (HttpContext.Session.Keys.Count() != 0)
            {
                currentuser = _users.GetById(HttpContext.Session.GetInt32("userid").Value);
            }
            if (currentuser.IsBlocked || currentuser.Id == 0)
            {
                return RedirectToAction("Authorization", "Auth");
            }
            _users.DeleteUsers(userIds);
            return RedirectToAction("Users");
        }

        [HttpPost]
        public IActionResult block([FromBody] List<int> userIds)
        {
            User currentuser = new User();
            if (HttpContext.Session.Keys.Count() != 0)
            {
                currentuser = _users.GetById(HttpContext.Session.GetInt32("userid").Value);
            }
            if (currentuser.IsBlocked || currentuser.Id == 0)
            {
                return RedirectToAction("Authorization", "Auth");
            }
            _users.BlockUsers(userIds);
            return RedirectToAction("Users");
        }

        [HttpPost]
        public IActionResult unblock([FromBody] List<int> userIds)
        {
            User currentuser = new User();
            if (HttpContext.Session.Keys.Count() != 0)
            {
                currentuser = _users.GetById(HttpContext.Session.GetInt32("userid").Value);
            }
            if (currentuser.IsBlocked || currentuser.Id == 0)
            {
                return RedirectToAction("Authorization", "Auth");
            }
            _users.UnblockUsers(userIds);
            return RedirectToAction("Users");
        }
    }
}
