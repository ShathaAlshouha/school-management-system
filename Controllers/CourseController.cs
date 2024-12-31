using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; 
using SchoolProject.Data;
 

namespace SchoolProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CourseController(ApplicationDBContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            var courses = _context.Courses .Include(c => c.CourseTeacher) 
                   .ThenInclude(t => t.TeacherPerson) 
                    .Include(c => c.CourseClass);

            return View(courses.ToList());

        }


        public IActionResult Create()
        {
            var teacherList = _context.Teachers
                .Include(t => t.TeacherPerson)
                .Select(t => new
                {
                    TeacherId = t.TeacherId,
                    FullName = t.TeacherPerson.FirstName + " " + t.TeacherPerson.LastName
                }).ToList();

            if (!teacherList.Any())
            {
                TempData["ErrorMessage"] = "No teachers available";
                return RedirectToAction("Index", "Teachers");
            }

            ViewBag.TeacherId = new SelectList(teacherList, "TeacherId", "FullName");
          ViewBag.ClassId= new SelectList(_context.Classes, "ClassId", "ClassName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

           
            var teacherList = _context.Teachers
                .Include(t => t.TeacherPerson)
                .Select(t => new
                {
                    TeacherId = t.TeacherId,
                    FullName = t.TeacherPerson.FirstName + " " + t.TeacherPerson.LastName
                }).ToList();

            if (!teacherList.Any())
            {
                TempData["ErrorMessage"] = "No teachers available";
                return RedirectToAction("Index", "Teachers");
            }

            ViewBag.TeacherId = new SelectList(teacherList, "TeacherId", "FullName", course.TeacherId);
            ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassName", course.ClassId);

            return View(course);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = _context.Classes.Find(id);

            if (cours == null)
            {
                return NotFound();
            }

            var teacherList = _context.Teachers
                .Include(t => t.TeacherPerson)
                .Select(t => new
                {
                    TeacherId = t.TeacherId,
                    FullName = t.TeacherPerson.FirstName + " " + t.TeacherPerson.LastName 
                }).ToList();

            var classList = _context.Classes.ToList();

            ViewBag.TeacherId = new SelectList(teacherList, "TeacherId", "FullName");
            ViewBag.ClassId = new SelectList(classList, "ClassId", "ClassName");
            return View(cours);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Course course)
        {
            if (id != course.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }


            var teacherList = _context.Teachers
                .Include(t => t.TeacherPerson)
                .Select(t => new
                {
                    TeacherId = t.TeacherId,
                    FullName = t.TeacherPerson.FirstName + " " + t.TeacherPerson.LastName
                }).ToList();
           
            ViewBag.TeacherId = new SelectList(teacherList, "TeacherId", "FullName");
            ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassName");
            return View(course);
        }

     
       

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var Course = _context.Courses.
                    FirstOrDefault(c => c.CourseId == id);

                if (Course == null)
                {
                    return NotFound();
                }


                _context.Courses.Remove(Course);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return BadRequest($"  Deletion failed: {ex.Message}");
            }
        }
        private bool CourseExists(int id)
        {
            return _context.Courses.Any(c => c.CourseId == id);
        }
    }
}
