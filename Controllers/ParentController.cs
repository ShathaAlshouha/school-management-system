using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;
using SchoolProject.Helper;

namespace SchoolProject.Controllers
{
    public class ParentController : Controller
    {
        public readonly ApplicationDBContext _context;
        public ParentController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchQuery)
        {
            if (!SessionHelpercs.IsAdmin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }

            var staffList = _context.Parents
                .Include(p=>p.ParentPerson)
                .AsQueryable();

            // إذا كان هناك نص بحث، نقوم بتصفية النتائج بناءً على الاسم أو البريد الإلكتروني
            if (!string.IsNullOrEmpty(searchQuery))
            {
                staffList = staffList.Where(s => s.ParentPerson.FirstName.Contains(searchQuery) ||
                                                  s.ParentPerson.LastName.Contains(searchQuery) ||
                                                  s.ParentPerson.Email.Contains(searchQuery));
            }


            var parentDtoList = staffList.Select(p => new ParentDto
            {
                PersonID = p.ParentPerson.PersonID,
                ParentID = p.ParentPerson.PersonID,
                NationalID = p.ParentPerson.NationalID,
                FirstName = p.ParentPerson.FirstName,
                SecondName = p.ParentPerson.SecondName,
                LastName = p.ParentPerson.LastName,
                Email = p.ParentPerson.Email,

                contactNumber = p.ParentPerson.contactNumber,
                Address = p.ParentPerson.Address,
                DateOfBirth = p.ParentPerson.DateOfBirth,
                Role = p.ParentPerson.Role,
                RelationshipToStudent= p.RelationshipToStudent,


            }).ToList();

            return View(parentDtoList);
        }

       

        public IActionResult Create(int Id)
        {
            if (!SessionHelpercs.IsAdmin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }

            int studentId = (int)TempData["StudentId"];

		
			ViewData["StudentId"] = studentId;
			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ParentDto parentDto, int studentId)
        {
            if (ModelState.IsValid)
            {
              
                var person = new Person()
                {
                    NationalID = parentDto.NationalID,
                    FirstName = parentDto.FirstName,
                    SecondName = parentDto.SecondName,
                    LastName = parentDto.LastName,
                    Email = parentDto.Email,
                    contactNumber = parentDto.contactNumber,
                    Address = parentDto.Address,
                    DateOfBirth = parentDto.DateOfBirth,
                    Role = "Role",
                };

                _context.Persons.Add(person);
                _context.SaveChanges();

              
                var parent = new Parent
                {
                    ParentID = parentDto.ParentID,
                    PersonID = person.PersonID,
                    RelationshipToStudent = parentDto.RelationshipToStudent
                };

                _context.Parents.Add(parent);
                _context.SaveChanges();

                
             var student = _context.Students.FirstOrDefault(s => s.StudentId ==  studentId);
                if (student != null && parent != null)
                {
                    student.ParentId = parent.ParentID;
                    _context.Students.Update(student);
                    _context.SaveChanges();
                }


               
                return RedirectToAction("Index", "Student");
            }

            return View(parentDto);
        }

    }
}
