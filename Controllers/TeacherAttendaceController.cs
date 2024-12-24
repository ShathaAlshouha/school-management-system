using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data;

namespace SchoolProject.Controllers
{
    public class TeacherAttendaceController : Controller
    {
        private readonly ApplicationDBContext context;

        public TeacherAttendaceController(ApplicationDBContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime date)
        {
            // Get the current time
            var currentTime = DateTime.Now.TimeOfDay;

            // Determine the attendance status based on the time
            string status;
            if (currentTime >= new TimeSpan(8, 0, 0) && currentTime < new TimeSpan(9, 0, 0))
            {
                status = "Present";  // Between 8:00 AM and 9:00 AM
            }
            else if (currentTime >= new TimeSpan(15, 0, 0) && currentTime < new TimeSpan(16, 0, 0))
            {
                status = "Absent";  // Between 3:00 PM and 4:00 PM
            }
            else
            {
                status = "Late";  // Any other time
            }

            // Create the attendance record
            var attendance = new TeacherAttendance
            {
                date = date,
                status = status
            };

            // Save to the database
            context.Add(attendance);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
    

