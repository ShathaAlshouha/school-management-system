using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;

namespace SchoolProject.Controllers
{
    public class UsersController : Controller
    {

        public readonly ApplicationDBContext context;

        public UsersController(ApplicationDBContext context)
        {
            this.context = context;

        }
        public IActionResult Index(string searchQuery)
        {

            var users = context.Users
                .Include(u => u.UserPerson)
                .AsQueryable();


            if (!string.IsNullOrEmpty(searchQuery))
            {
                users = users.Where(u => u.Username.Contains(searchQuery) ||

                                         (u.UserPerson.FirstName.Contains(searchQuery) ||
                                          u.UserPerson.LastName.Contains(searchQuery)));  // البحث عن الاسم أو اسم المستخدم
            }

            // تحويل النتيجة إلى UserDto ثم إرجاعها إلى الواجهة
            var userList = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                PersonID = u.UserPerson.PersonID,
                Username = u.Username,
                Password = u.Password,  
                FirstName = u.UserPerson.FirstName,
                LastName = u.UserPerson.LastName,
                Email = u.UserPerson.Email,
                contactNumber = u.UserPerson.contactNumber,
                Address = u.UserPerson.Address,
                DateOfBirth = u.UserPerson.DateOfBirth,
                Role = u.UserPerson.Role,
            }).ToList();

            return View(userList);
        }

        public IActionResult Create()
        {
            var userDto = new UserDto
            {
                PersonId = TempData["PersonID"] != null ? (int)TempData["PersonID"] : 0,

                NationalID = TempData["NationalID"] != null ? (int)TempData["NationalID"] : 0,
                FirstName = TempData["FirstName"]?.ToString() ?? string.Empty,
                SecondName = TempData["SecondName"]?.ToString() ?? string.Empty,
                LastName = TempData["LastName"]?.ToString() ?? string.Empty,
                Email = TempData["Email"]?.ToString() ?? string.Empty,
                contactNumber = TempData["ContactNumber"] != null ? (int)TempData["ContactNumber"] : 0,
                Address = TempData["Address"]?.ToString() ?? string.Empty,
                DateOfBirth = TempData["DateOfBirth"] != null
        ? DateTime.Parse(TempData["DateOfBirth"].ToString())
        : (DateTime?)null,
                Role = TempData["Role"]?.ToString() ?? string.Empty

            }; 

            return View(userDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {

                var user = new User
                {
                    PersonId = userDto.PersonId,
                    Username = userDto.Username,
                    Password = userDto.Password,
                };

                context.Users.Add(user);
                context.SaveChanges();

                return RedirectToAction("Index", "Users");
            }

            return View(userDto);
        }

        public IActionResult Edit(int id)
        {
            var user = context.Users
                .Include(u => u.UserPerson)
                .FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                UserId = user.UserId,
                PersonID = user.UserPerson.PersonID,
                Username = user.Username,
                Password = user.Password,
                FirstName = user.UserPerson.FirstName,
                SecondName = user.UserPerson.SecondName,
                LastName = user.UserPerson.LastName,
                Email = user.UserPerson.Email,
                contactNumber = user.UserPerson.contactNumber,
                Address = user.UserPerson.Address,
                DateOfBirth = user.UserPerson.DateOfBirth,
                Role = user.UserPerson.Role
            };

            return View(userDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                var user = context.Users
                    .Include(u => u.UserPerson)
                    .FirstOrDefault(u => u.UserId == userDto.UserId);

                if (user == null)
                {
                    return NotFound();
                }
                user.Username = userDto.Username;
                user.UserPerson.FirstName = userDto.FirstName;
                user.UserPerson.SecondName = userDto.SecondName;
                user.UserPerson.LastName = userDto.LastName;
                user.UserPerson.Email = userDto.Email;
                user.UserPerson.contactNumber = userDto.contactNumber;
                user.UserPerson.Address = userDto.Address;
                user.UserPerson.DateOfBirth = userDto.DateOfBirth;
                user.UserPerson.Role = userDto.Role;

                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(userDto);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = context.Users
                    .Include(u => u.UserPerson)
                    .FirstOrDefault(u => u.UserId == id);

                if (user == null)
                {
                    return NotFound();
                }

                if (user.UserPerson != null)
                {
                    context.Persons.Remove(user.UserPerson);  
                } 

                context.Users.Remove(user);  
                context.Users.Remove(user);  
                 
                context.SaveChanges();  

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred while deleting: {ex.Message}");
            }



        }
        public IActionResult Details(int id)
        {
            var user = context.Users
                .Include(u => u.UserPerson) // 
                .FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound(); // إذا لم يتم العثور على المستخدم
            }

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.UserPerson.FirstName,
                SecondName = user.UserPerson.SecondName,
                LastName = user.UserPerson.LastName,
                Email = user.UserPerson.Email,
                contactNumber = user.UserPerson.contactNumber,
                Address = user.UserPerson.Address,
                DateOfBirth = user.UserPerson.DateOfBirth,
                Role = user.UserPerson.Role
            };

            return View(userDto); // عرض صفحة التفاصيل مع البيانات
        }
    }

}
    

