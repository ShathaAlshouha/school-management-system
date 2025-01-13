using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SchoolProject.Data;
using SchoolProject.Helper;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AccountController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "يرجى إدخال اسم المستخدم وكلمة المرور.";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                ViewBag.ErrorMessage = "اسم المستخدم أو كلمة المرور غير صحيحة!";
                return View();
            }

            var person = _context.Persons.FirstOrDefault(p => p.PersonID == user.PersonId); 
            if (person == null)
            {
                ViewBag.ErrorMessage = "لا يمكن العثور على بيانات الشخص المرتبطة!";
                return View();
            }

            HttpContext.Session.SetInt32("PersonID", person.PersonID);

            switch (person.Role)
            {
                case "Admin":
                    SessionHelpercs.SetAsUser(HttpContext);
                    return RedirectToAction("Index", "Admin");

                case "Teacher":

                    SessionHelpercs.SetAsTeacher(HttpContext);
                   var teacher = _context.Teachers.
                        FirstOrDefault(t => t.PersonId == person.PersonID);
                    if (teacher == null)
                    {
                        ViewBag.ErrorMessage = "لا يمكن العثور على بيانات المعلم المرتبطة!";
                        return View();
                    }
                    return RedirectToAction("Index", "TeacherHome", new { Id = teacher.TeacherId }); 

                default:
                    ViewBag.ErrorMessage = "الدور غير معرّف.";
                    return View();
            }
        }


        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Index","LandingHome"); 
        }

      

    }
}

