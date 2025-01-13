using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data;
using SchoolProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Helper;

namespace SchoolProject.Controllers
{
    public class AdminController : Controller
    {
         private readonly ApplicationDBContext _context;

       public AdminController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
          
            if (!SessionHelpercs.IsAdmin(HttpContext))
            {
                return RedirectToAction("Login", "Account"); 
            }

            var personId = HttpContext.Session.GetInt32("PersonID");
            if (personId == null)
            {
                return RedirectToAction("Login", "Account"); 
            }

            var user = _context.Persons.FirstOrDefault(p => p.PersonID == personId);
            if (user == null)
            {
                return NotFound(); 
            }


            var model = new DashboardViewModel
            {
                NumberOfStudents = _context.Students.Count(),
                NumberOfTeachers = _context.Teachers.Count(),
                NumberOfStaffs = _context.Staffs.Count(),
                DateTime = DateTime.Now,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(model);
        }


    }
}