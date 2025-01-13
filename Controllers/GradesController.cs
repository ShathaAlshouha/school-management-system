using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;

namespace SchoolProject.Controllers
{
    public class GradesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public GradesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Grades
        public IActionResult  Index()
        {
            var applicationDBContext = _context.Grades.Include(g => g.GradeCourse).Include(g => g.GradeStudent);
            return View( applicationDBContext.ToList());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade =  _context.Grades
                .Include(g => g.GradeCourse)
                .Include(g => g.GradeStudent)
                .FirstOrDefault(m => m.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }


        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", grade.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", grade.StudentId);
            return View(grade);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade =  _context.Grades.Find(id);
            if (grade == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", grade.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", grade.StudentId);
            return View(grade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Grade grade)
        {
            if (id != grade.GradeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                     _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.GradeId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", grade.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", grade.StudentId);
            return View(grade);
        }

        // GET: Grades/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade =  _context.Grades
                .Include(g => g.GradeCourse)
                .Include(g => g.GradeStudent)
                .FirstOrDefault(m => m.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult  DeleteConfirmed(int id)
        {
            var grade =  _context.Grades.Find(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
            }

             _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.GradeId == id);
        }
    }
}
