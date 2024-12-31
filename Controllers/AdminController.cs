using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class AdminController : Controller
    {
        public readonly ApplicationDBContext _context;
        public AdminController(ApplicationDBContext context)
        {

            _context = context;
        }
        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                NumberOfStudents = _context.Students.Count(),
                NumberOfTeachers = _context.Teachers.Count(),
                NumberOfStaffs = _context.Staffs.Count(),
                DateTime = DateTime.Now,
            };

            return View(model);
        }
    }
}
