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
    public class PaymentsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PaymentsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Payments.Include(p => p.PaymentClass).Include(p => p.PaymentStudent);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.PaymentClass)
                .Include(p => p.PaymentStudent)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                 _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName", payment.ClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", payment.StudentId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment =  _context.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName", payment.ClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", payment.StudentId);
            return View(payment);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName", payment.ClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", payment.StudentId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment =  _context.Payments
                .Include(p => p.PaymentClass)
                .Include(p => p.PaymentStudent)
                .FirstOrDefault(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var payment =  _context.Payments.Find(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}
