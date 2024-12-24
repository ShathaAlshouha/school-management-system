using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using SchoolProject.Data;
 

namespace SchoolProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDBContext context;

        public CourseController(ApplicationDBContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var courses = context.Courses.OrderByDescending(p => p.CourseId).ToList();
            return View(courses);
        }

        public IActionResult Create()
        {
            return View(new CourseDto()); 
        }

        [HttpPost]
        public IActionResult Create(CourseDto courseDto)
        {
            if (ModelState.IsValid)
            {
                var newCourse = new Course
                {
                    CourseName = courseDto.CourseName
                };
                context.Courses.Add(newCourse);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseDto);
        }

        public IActionResult Edit(int courseId)
        {
            var course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                return RedirectToAction("Index");
            }

            var courseDto = new CourseDto
            {
                CourseName = course.CourseName
            };

            return View(courseDto);
        }


        [HttpPost]
        public IActionResult Edit(int courseId, CourseDto courseDto)
        {
            var course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                course.CourseName = courseDto.CourseName;

                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(courseDto);
        }

        public IActionResult Delete(int courseId)
        {
            var course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                return RedirectToAction("Index");
            }

            context.Courses.Remove(course);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
