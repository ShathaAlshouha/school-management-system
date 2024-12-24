using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;

namespace SchoolProject.Controllers
{
    public class GradeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public GradeController(ApplicationDBContext context)
        {
            _context = context;
        }

    
        public IActionResult Index()
        {
            var grades = _context.Grades
                .Include(g => g.GradeStudent)
                .Include(g => g.GradeCourse)
                .ToListAsync();

            return View(grades);
        }

        public IActionResult Create()
        {
           
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GradeDto model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

           
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View(model);
        }

    
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = _context.Grades.Find(id);
            if (grade == null)
            {
                return NotFound();
            }

           
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View(grade);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, GradeDto model)
        {

           
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View(model);
        }

      
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = _context.Grades
                .Include(g => g.GradeStudent)
                .Include(g => g.GradeCourse)
                  .FirstOrDefaultAsync(m => m.GradeId == id);

            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var grade = _context.Grades.Find(id);
            if (grade != null)
            {   
                _context.Grades.Remove(grade);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.GradeId == id);
        }


    }
}