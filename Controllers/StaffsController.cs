using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;

namespace SchoolProject.Controllers
{
    public class StaffsController : Controller
    {
        public readonly ApplicationDBContext _context;
        public StaffsController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchQuery)
        {
            var staffList = _context.Staffs
                .Include(s => s.StaffPerson)
                .AsQueryable();

            
            if (!string.IsNullOrEmpty(searchQuery))
            {
                staffList = staffList.Where(s => s.StaffPerson.FirstName.Contains(searchQuery) ||
                                                  s.StaffPerson.LastName.Contains(searchQuery) ||
                                                  s.StaffPerson.Email.Contains(searchQuery));
            }

            
            var staffDtoList = staffList.Select(s => new StaffDto
            {
                PersonId = s.StaffPerson.PersonID,
                StaffId = s.StaffId,
                NationalID = s.StaffPerson.NationalID,
                FirstName = s.StaffPerson.FirstName,
                SecondName = s.StaffPerson.SecondName,
                LastName = s.StaffPerson.LastName,
                Email = s.StaffPerson.Email,
                
                ContactNumber = s.StaffPerson.contactNumber,
                Address = s.StaffPerson.Address,
                DateOfBirth = s.StaffPerson.DateOfBirth,
                Role = s.StaffPerson.Role,
                Salary = s.Salary,
                EmploymentStartDate = s.EmploymentStartDate
            }).ToList();

            return View(staffDtoList);
        }

      

        public IActionResult Create ()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateWithUserAndPass(StaffDto staffDto)
        {
            if (ModelState.IsValid)
            {

                var person = new Person()
                {
                    NationalID = staffDto.NationalID,
                    FirstName = staffDto.FirstName,
                    SecondName = staffDto.SecondName,
                    LastName = staffDto.LastName,
                    Email = staffDto.Email,
                    contactNumber = staffDto.ContactNumber,
                    Address = staffDto.Address,
                    DateOfBirth = staffDto.DateOfBirth,
                    Role = staffDto.Role,
                };

                _context.Persons.Add(person);
                _context.SaveChanges();


                var staff = new Staff
                {
                    PersonId = person.PersonID,
                    Salary = staffDto.Salary,
                    EmploymentStartDate = DateTime.Now,
                };

                _context.Staffs.Add(staff);
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

            return View(staffDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateWithoutUserAndPass(StaffDto staffDto)
        {

            if (ModelState.IsValid)
            {
                var person = new Person()
                {
                    NationalID = staffDto.NationalID,
                    FirstName = staffDto.FirstName,
                    SecondName = staffDto.SecondName,
                    LastName = staffDto.LastName,
                    Email = staffDto.Email,
                    contactNumber = staffDto.ContactNumber,
                    Address = staffDto.Address,
                    DateOfBirth = staffDto.DateOfBirth,
                    Role = staffDto.Role,

                };

                _context.Persons.Add(person);
                _context.SaveChanges();


                var staff = new Staff
                {
                    PersonId = person.PersonID,
                    Salary = staffDto.Salary,
                    EmploymentStartDate = DateTime.Now,
                };

                _context.Staffs.Add(staff);
                _context.SaveChanges();
                return RedirectToAction("Index", "Staffs");  
            }

            return View(staffDto);
        }
        public IActionResult Edit(int id)
        {

            var staff = _context.Staffs
        .Include(s => s.StaffPerson) 
        .FirstOrDefault(s => s.StaffId == id);

            if (staff == null)
            {
                return NotFound();
            }
            var staffDto = new StaffDto()
            {
                StaffId = staff.StaffId,
                PersonId = staff.PersonId,
                NationalID = staff.StaffPerson.NationalID,
                FirstName = staff.StaffPerson.FirstName,
                SecondName = staff.StaffPerson.SecondName,
                LastName = staff.StaffPerson.LastName,
                Email = staff.StaffPerson.Email,
                ContactNumber = staff.StaffPerson.contactNumber,
                Address = staff.StaffPerson.Address,
                DateOfBirth = staff.StaffPerson.DateOfBirth,
                Role = staff.StaffPerson.Role,
                Salary = staff.Salary,
                EmploymentStartDate = staff.EmploymentStartDate
            }; 

            return View(staffDto);  
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StaffDto staffDto)
        {
            if (ModelState.IsValid)
            {
                var staff = _context.Staffs
                    .Include(s => s.StaffPerson)
                    .FirstOrDefault(s => s.StaffId == staffDto.StaffId);
                if (staff == null)
                {
                    return NotFound();
                }
                staff.StaffPerson.NationalID = staffDto.NationalID;
                staff.StaffPerson.FirstName = staffDto.FirstName;
                staff.StaffPerson.SecondName = staffDto.SecondName;
                staff.StaffPerson.LastName = staffDto.LastName;
                staff.StaffPerson.Email = staffDto.Email;
                staff.StaffPerson.contactNumber = staffDto.ContactNumber;
                staff.StaffPerson.Address = staffDto.Address;
                staff.StaffPerson.Role = staffDto.Role;
                staff.StaffPerson.DateOfBirth = staffDto.DateOfBirth;

                staff.Salary = staffDto.Salary;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffDto); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {              
                var staff = _context.Staffs.Include(s => s.StaffPerson).
                    FirstOrDefault(s => s.StaffId == id);

                if (staff == null)
                {
                    return NotFound();
                }

                _context.Persons.Remove(staff.StaffPerson);
                _context.Staffs.Remove(staff);
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
