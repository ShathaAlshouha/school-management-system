using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;
using SchoolProject.Helper;

namespace SchoolProject.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PaymentsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string searchQuery)
        {
            if (!SessionHelpercs.IsAdmin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }

            var paymentsList = _context.Payments
                .Include(p => p.PaymentClass)
                .Include(p => p.PaymentStudent)
                .ThenInclude(p => p.StudentPerson)
                .AsQueryable();

        
            if (!string.IsNullOrEmpty(searchQuery))
            {
                paymentsList = paymentsList.Where(s =>
                    s.PaymentStudent.StudentPerson.FirstName.Contains(searchQuery) ||
                    s.PaymentStudent.StudentPerson.LastName.Contains(searchQuery)); 
            }

            // تنفيذ الاستعلام وإرجاع النتائج إلى العرض
            return View(paymentsList.ToList());
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!SessionHelpercs.IsAdmin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }

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
        public IActionResult StudentIndex(string searchQuery)
        {
            if (!SessionHelpercs.IsAdmin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }

            var studentList = _context.Students
                .Include(s => s.StudentPerson)
                .Include(p => p.StudentParent)
                .Include(c => c.StudentClass)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                studentList = studentList.Where(s =>
                    s.StudentPerson.FirstName.Contains(searchQuery) ||
                    s.StudentPerson.LastName.Contains(searchQuery) ||
                    s.StudentPerson.Email.Contains(searchQuery));
            }

        

            return View(studentList.ToList());
        }


        [HttpGet]

        public IActionResult Create(int id )
        {
            if (!SessionHelpercs.IsAdmin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }

            var student = _context.Students.Include(s => s.StudentPerson).
                Include(p=>p.Payment).
                Include(c => c.StudentClass).FirstOrDefault(s => s.StudentId == id); 
            
            if (student == null)
            {
                return NotFound(); 
            }
            var latestPayment = student.Payment
                .OrderByDescending(p => p.PaymentDate) 
                     .FirstOrDefault();

            var paymentDto = new PaymentDto()
            {
                StudentId = student.StudentId,
                FirstName = student.StudentPerson.FirstName,
                LastName = student.StudentPerson.LastName,
                TotalAmount = student.StudentClass.fees,
                ClassId = student.ClassId,
                RemainingPayment = latestPayment?.RemainingPayment ?? 0,

            }; 


            return View(paymentDto);  
        }


        [HttpPost]
        public IActionResult Create(PaymentDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(paymentDto); 
            }

          
            var remainingPayment = paymentDto.TotalAmount - paymentDto.PaymentAmount;

            var payment = new Payment
            {
                StudentId = paymentDto.StudentId,
                ClassId = paymentDto.ClassId,
                PaymentAmount = paymentDto.PaymentAmount,
                TotalAmount = paymentDto.TotalAmount,
                PaymentDate = DateTime.Now,
                RemainingPayment=remainingPayment,
            };

          
            _context.Payments.Add(payment);
            _context.SaveChanges();

           
            return RedirectToAction("Index");
        }



        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var payment =  _context.Payments.Find(id);
        //    if (payment == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName", payment.ClassId);
        //    ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", payment.StudentId);
        //    return View(payment);
        //}

     
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id,  Payment payment)
        //{
        //    if (id != payment.PaymentId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(payment);
        //             _context.SaveChanges();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PaymentExists(payment.PaymentId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName", payment.ClassId);
        //    ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", payment.StudentId);
        //    return View(payment);
        //}

        // GET: Payments/Delete/5
        
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
