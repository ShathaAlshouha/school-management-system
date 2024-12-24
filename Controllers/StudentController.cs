using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;

namespace SchoolProject.Controllers
{
    public class StudentController : Controller
    {
        public readonly ApplicationDBContext _context;
        public StudentController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchQuery)
        {

            var studentList = _context.Students
               .Include(s => s.StudentPerson)
               .Include(p => p.StudentParent)
               .Include(c => c.StudentClass)
                     .AsQueryable(); 

            if (!string.IsNullOrEmpty(searchQuery))
            {
                studentList = studentList.Where(s => s.StudentPerson.FirstName.Contains(searchQuery) ||
                              s.StudentPerson.LastName.Contains(searchQuery) ||
                              s.StudentPerson.Email.Contains(searchQuery));
            }

            return View(studentList);
        }

        public IActionResult Create()
        {
            var classList = _context.Classes.ToList();
            if (!classList.Any())
            {
                
                TempData["ErrorMessage"] = "No categories available. Please create a category first.";
                return RedirectToAction("Index", "Categories"); 
            }

       
            ViewData["Classes"] = classList;
            return View();
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentDto studentDto)
        {
            if (ModelState.IsValid)
            {
          
                var person = new Person()
                {
                    NationalID = studentDto.NationalID,
                    FirstName = studentDto.FirstName,
                    SecondName = studentDto.SecondName,
                    LastName = studentDto.LastName,
                    Email = studentDto.Email,
                    contactNumber = studentDto.contactNumber,
                    Address = studentDto.Address,
                    DateOfBirth = studentDto.DateOfBirth,
                    Role = "Student",
                };

                _context.Persons.Add(person);
                _context.SaveChanges();

               
                var student = new Student()
                {
                    PersonId = person.PersonID,
                    ClassId = studentDto.ClassId,
                    EnrollmentDate = DateTime.Now,
                    ParentId =null,
                };

                _context.Add(student);
                _context.SaveChanges();

				TempData["StudentId"] = student.StudentId;
				
				return RedirectToAction("Create", "Parent" );
            }

          
            return View(studentDto);
        }

		public IActionResult Details(int id)
		{
			var student = _context.Students
				.Include(s => s.StudentPerson)   
				.Include(p => p.StudentParent)  
				.ThenInclude(pp => pp.ParentPerson)
				.FirstOrDefault(s => s.StudentId == id);

			if (student == null)
			{
				return NotFound();
			}

		
			var parentDto = new ParentDto
			{
				ParentID = student.StudentParent.ParentID,
				NationalID = student.StudentParent.ParentPerson.NationalID,
				FirstName = student.StudentParent.ParentPerson.FirstName,
				SecondName = student.StudentParent.ParentPerson.SecondName,
				LastName = student.StudentParent.ParentPerson.LastName,
				Email = student.StudentParent.ParentPerson.Email,
				Address = student.StudentParent.ParentPerson.Address,
				DateOfBirth = student.StudentParent.ParentPerson.DateOfBirth,
				contactNumber = student.StudentParent.ParentPerson.contactNumber,
				RelationshipToStudent = student.StudentParent.RelationshipToStudent,
				Role = "Parent"
			};

			
			ViewData["Parent"] = parentDto;

            return View(parentDto); 
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			try
			{

				var student = _context.Students.Include(s => s.StudentPerson).
                    Include(p=>p.StudentParent).
					FirstOrDefault(s => s.StudentId == id);

				if (student == null)
				{
					return NotFound();
				}

				_context.Persons.Remove(student.StudentPerson);
                _context.Parents.Remove(student.StudentParent);
				_context.Students.Remove(student);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{

				return BadRequest($"  Deletion failed: {ex.Message}");
			}
		}

	}


}

