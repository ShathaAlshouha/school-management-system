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
    public class ClassesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ClassesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Classes
        public IActionResult Index()
        {
            var classList = _context.Classes
                .Include(c => c.ClassTeacher)
                .ThenInclude(t => t.TeacherPerson) 
                .ToList();

            return View(classList); 
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clas =  _context.Classes
                .Include(t => t.ClassTeacher)
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (clas == null)
            {
                return NotFound();
            }

            return View(clas);
        }

        // GET: Classes/Create
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
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Class clas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clas);
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

            
            ViewBag.TeacherId = new SelectList(teacherList, "TeacherId", "FullName", clas.TeacherId);

            return View(clas);
        }


        // GET: Classes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clas = _context.Classes.Find(id);

            if (clas == null)
            {
                return NotFound();
            }

            var teacherList = _context.Teachers
                .Include(t => t.TeacherPerson) 
                .Select(t => new
                {
                    TeacherId = t.TeacherId,
                    FullName = t.TeacherPerson.FirstName + " " + t.TeacherPerson.LastName // دمج الاسم الأول واسم العائلة
                }).ToList();

            ViewBag.TeacherId = new SelectList(teacherList, "TeacherId", "FullName", clas.TeacherId);
            return View(clas);
        }


   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Class clss)
        {
            if (id != clss.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clss);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(clss.ClassId))
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

            ViewBag.TeacherId = new SelectList(teacherList, "TeacherId", "FullName", clss.TeacherId);

            return View(clss);
        }


    private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}
