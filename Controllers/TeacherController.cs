using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data;
using Microsoft.EntityFrameworkCore;
namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        public readonly ApplicationDBContext _context;
        public TeacherController(ApplicationDBContext context)
        {
            _context = context;
        }
     
        public IActionResult Index(string searchQuery)
        {
            var teacherList = _context.Teachers
                .Include(t => t.TeacherPerson)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                teacherList = teacherList.Where(t => t.TeacherPerson.FirstName.Contains(searchQuery) ||
                                                     t.TeacherPerson.LastName.Contains(searchQuery) ||
                                                     t.TeacherPerson.Email.Contains(searchQuery));
            }

        
            var teacherDtoList = teacherList.Select(t => new TeacherDto
            {
                TeacherId = t.TeacherId,
                SubjectSpecialty = t.SubjectSpecialty,
                EmploymentStartDate = t.EmploymentStartDate,
                Salary = t.Salary,
                PersonID = t.TeacherPerson.PersonID,
                NationalID = t.TeacherPerson.NationalID,
                FirstName = t.TeacherPerson.FirstName,
                SecondName = t.TeacherPerson.SecondName,
                LastName = t.TeacherPerson.LastName,
                contactNumber = t.TeacherPerson.contactNumber,
                Email = t.TeacherPerson.Email,
                DateOfBirth = t.TeacherPerson.DateOfBirth,
                Address = t.TeacherPerson.Address,
                Role = t.TeacherPerson.Role
            }).ToList();

            return View(teacherDtoList);
        }

       
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateWithUserAndPass(TeacherDto teacherDto)
        {
            if (ModelState.IsValid)
            {
                var person = new Person()
                {
                    NationalID = teacherDto.NationalID,
                    FirstName = teacherDto.FirstName,
                    SecondName = teacherDto.SecondName,
                    LastName = teacherDto.LastName,
                    Email = teacherDto.Email,
                    contactNumber = teacherDto.contactNumber,
                    Address = teacherDto.Address,
                    DateOfBirth = teacherDto.DateOfBirth,
                    Role = teacherDto.Role,
                };

                _context.Persons.Add(person);
                _context.SaveChanges();

                var teacher = new Teacher()
                {
                    PersonId = person.PersonID,
                    SubjectSpecialty = teacherDto.SubjectSpecialty,
                    Salary = teacherDto.Salary,
                    EmploymentStartDate = DateTime.Now, 
                };

                _context.Teachers.Add(teacher);
                _context.SaveChanges();

                TempData["PersonID"] = person.PersonID;
                TempData["NationalID"] = person.NationalID;
                TempData["FirstName"] = person.FirstName;
                TempData["SecondName"] = person.SecondName;
                TempData["LastName"] = person.LastName;
                TempData["Email"] = person.Email;  
                TempData["ContactNumber"] = person.contactNumber;
                TempData["Address"] = person.Address;
                TempData["DateOfBirth"] = person.DateOfBirth;
                TempData["Role"] = person.Role;

                TempData.Keep();
                return RedirectToAction("Create", "Users");
            }

            return View(teacherDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateWithoutUserAndPass(TeacherDto teacherDto)
        {
            if (ModelState.IsValid)
            {
                var person = new Person()
                {
                    NationalID = teacherDto.NationalID,
                    FirstName = teacherDto.FirstName,
                    SecondName = teacherDto.SecondName,
                    LastName = teacherDto.LastName,
                    Email = teacherDto.Email,
                    contactNumber = teacherDto.contactNumber,
                    Address = teacherDto.Address,
                    DateOfBirth = teacherDto.DateOfBirth,
                    Role = teacherDto.Role
                };

                _context.Persons.Add(person);
                _context.SaveChanges();

                var teacher = new Teacher()
                {
                    PersonId = person.PersonID,
                    SubjectSpecialty = teacherDto.SubjectSpecialty,
                    Salary = teacherDto.Salary,
                    EmploymentStartDate =  DateTime.Now
                };

                _context.Teachers.Add(teacher);
                _context.SaveChanges();
                return RedirectToAction("Index", "Teacher");
            }

            return View(teacherDto);
        }

     
        public IActionResult Edit(int id)
        {
            var teacher = _context.Teachers
                .Include(t => t.TeacherPerson)
                .FirstOrDefault(t => t.TeacherId == id);

            if (teacher == null)
            {
                return NotFound();
            }

            var teacherDto = new TeacherDto()
            {
                TeacherId = teacher.TeacherId,
                PersonID = teacher.TeacherPerson.PersonID,
                NationalID = teacher.TeacherPerson.NationalID,
                FirstName = teacher.TeacherPerson.FirstName,
                SecondName = teacher.TeacherPerson.SecondName,
                LastName = teacher.TeacherPerson.LastName,
                Email = teacher.TeacherPerson.Email,
                contactNumber = teacher.TeacherPerson.contactNumber,
                Address = teacher.TeacherPerson.Address,
                DateOfBirth = teacher.TeacherPerson.DateOfBirth,
                Role = teacher.TeacherPerson.Role,
                SubjectSpecialty = teacher.SubjectSpecialty,
                Salary = teacher.Salary,
                EmploymentStartDate = DateTime.Now,
            };

            return View(teacherDto);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TeacherDto teacherDto)
        {
            if (ModelState.IsValid)
            {
                var teacher = _context.Teachers
                    .Include(t => t.TeacherPerson)
                    .FirstOrDefault(t => t.TeacherId == teacherDto.TeacherId);

                if (teacher == null)
                {
                    return NotFound();
                }

                teacher.TeacherPerson.NationalID = teacherDto.NationalID;
                teacher.TeacherPerson.FirstName = teacherDto.FirstName;
                teacher.TeacherPerson.SecondName = teacherDto.SecondName;
                teacher.TeacherPerson.LastName = teacherDto.LastName;
                teacher.TeacherPerson.Email = teacherDto.Email;
                teacher.TeacherPerson.contactNumber = teacherDto.contactNumber;
                teacher.TeacherPerson.Address = teacherDto.Address;
                teacher.TeacherPerson.Role = teacherDto.Role;
                teacher.TeacherPerson.DateOfBirth = teacherDto.DateOfBirth;

                teacher.SubjectSpecialty = teacherDto.SubjectSpecialty;
                teacher.Salary = teacherDto.Salary;
                teacher.EmploymentStartDate = teacherDto.EmploymentStartDate ?? DateTime.Now;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacherDto);
        }

        // حذف معلم
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var teacher = _context.Teachers
                    .Include(t => t.TeacherPerson)
                    .FirstOrDefault(t => t.TeacherId == id);

                if (teacher == null)
                {
                    return NotFound();
                }

                _context.Persons.Remove(teacher.TeacherPerson);
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest($"Deletion failed: {ex.Message}");
            }
        }


        public IActionResult Details(int id)
        {
            var teacher = _context.Teachers
                .Include(t => t.TeacherPerson) // 
                .FirstOrDefault(t => t.TeacherId == id);

            if (teacher == null)
            {
                return NotFound();
            }

            var teacherDto = new TeacherDto
            {
                FirstName = teacher.TeacherPerson.FirstName,
                SecondName = teacher.TeacherPerson.SecondName,
                LastName = teacher.TeacherPerson.LastName,
                Email = teacher.TeacherPerson.Email,
                contactNumber = teacher.TeacherPerson.contactNumber,
                Address = teacher.TeacherPerson.Address,
                DateOfBirth = teacher.TeacherPerson.DateOfBirth,
                Role = "Teacher",
                Salary = teacher.Salary,
                EmploymentStartDate = teacher.EmploymentStartDate,
                SubjectSpecialty = teacher.SubjectSpecialty,


            };

            return View(teacherDto);
        }

    }
}
