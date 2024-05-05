using Microsoft.AspNetCore.Mvc;
using Task_4.Interfaces;
using Task4DataBase;
using Task4DataBase.Models;

namespace Task_4.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUser _users;
        public RegistrationController(IUser users) { _users = users; }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [Route("/Reg")]
        [ActionName("Reg")]
        public IActionResult Reg(IFormCollection RegValues) 
        {
            var newuser = new User()
            {
                Name = RegValues["Name"],
                Email = RegValues["Email"],
                IsBlocked = false,
                Password = RegValues["Password"].ToString().CalculateHash(),
                RegistrationDate = DateTime.Now,
            };
            if(!_users.GetAll().Any(c => c == newuser)) 
            {
                _users.AddUser(newuser); 
            }
            return RedirectToAction("Authorization", "Auth");
        }
    }
}
