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
            var applicationDBContext = _context.Classes.Include(c => c.ClassTeacher);
            return View( applicationDBContext.ToList());
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
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "SubjectSpecialty");
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
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "SubjectSpecialty", clas.TeacherId);
            return View(clas);
        }

        // GET: Classes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clas =  _context.Classes.Find(id);
            if (clas == null)
            {
                return NotFound();
            }
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "SubjectSpecialty", clas.TeacherId);
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
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "SubjectSpecialty", clss.TeacherId);
            return View(clss);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clss =  _context.Classes
                .Include(t => t.ClassTeacher)
                .FirstOrDefault(m => m.ClassId == id);
            if (clss == null)
            {
                return NotFound();
            }

            return View(clss);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var @class =  _context.Classes.Find(id);
            if (@class != null)
            {
                _context.Classes.Remove(@class);
            }

             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}
