using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;
using SchoolProject.Helper;



namespace SchoolProject.Controllers
{
    public class TeacherHomeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TeacherHomeController(ApplicationDBContext context)
        {
            _context = context;
        }
   

        public IActionResult Index(int Id)
        {

            if (!SessionHelpercs.IsTeacher(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }

            var teacherCourses = _context.Courses
                .Include(t => t.CourseTeacher)
                .ThenInclude(t => t.TeacherPerson)
                 .Where(c => c.TeacherId == Id)
                 .Include(c => c.CourseClass)
                 .ToList();

            if (teacherCourses == null || !teacherCourses.Any())
            {
                return NotFound("No courses found for this teacher.");
            }
            var teacherName = teacherCourses.FirstOrDefault()?.CourseTeacher?.TeacherPerson?.FirstName
                + " " + teacherCourses.FirstOrDefault()?.CourseTeacher?.TeacherPerson?.LastName;

            ViewData["TeacherName"] = teacherName;
            return View(teacherCourses);
        }



        public IActionResult ManageGrades(int courseId)
        {
            if (!SessionHelpercs.IsTeacher(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }

            var studentsWithGrades = (
               from student in _context.Students
               join course in _context.Courses
                   on student.ClassId equals course.ClassId
               where course.CourseId == courseId
               join grade in _context.Grades
                   on student.StudentId equals grade.StudentId into gradeGroup
               from grade in gradeGroup.DefaultIfEmpty()
               where grade == null || grade.CourseId == courseId // إضافة شرط للتأكد من مطابقة الكورس
               select new GradeDto
               {
                   StudentId = student.StudentId,
                   FirstName = student.StudentPerson.FirstName,
                   LastName = student.StudentPerson.LastName,
                   FirstExam = grade != null ? grade.FirstExam : null,
                   ActivityMark = grade != null ? grade.ActivityMark : null,
                   FinalExam = grade != null ? grade.FinalExam : null,
                   TotalGrade = grade != null ? grade.TotalGrade : null,
                   CourseId = courseId,
               }).ToList();

            ViewBag.TeacherId = _context.Courses.FirstOrDefault(c => c.CourseId == courseId)?.TeacherId;
            return View(studentsWithGrades);
        }


        [HttpPost]
        public IActionResult SaveGrades(Dictionary<int, GradeDto> grades)
        {
            if (grades != null && grades.Any())
            {
                foreach (var gradeDto in grades.Values)
                {
                    var existingGrade = _context.Grades
                        .FirstOrDefault(g => g.StudentId == gradeDto.StudentId && g.CourseId == gradeDto.CourseId);

                    if (existingGrade != null)
                    {
                      
                        existingGrade.FirstExam = gradeDto.FirstExam;
                        existingGrade.ActivityMark = gradeDto.ActivityMark;
                        existingGrade.FinalExam = gradeDto.FinalExam;
                        existingGrade.TotalGrade = (existingGrade.FirstExam ?? 0) +
                                                   (existingGrade.ActivityMark ?? 0) +
                                                   (existingGrade.FinalExam ?? 0);
                    }
                    else
                    {
                       
                        var newGrade = new Grade
                        {
                            StudentId = gradeDto.StudentId,
                            CourseId = gradeDto.CourseId,
                            FirstExam = gradeDto.FirstExam,
                            ActivityMark = gradeDto.ActivityMark,
                            FinalExam = gradeDto.FinalExam,
                            TotalGrade = (gradeDto.FirstExam ?? 0) +
                                         (gradeDto.ActivityMark ?? 0) +
                                         (gradeDto.FinalExam ?? 0)
                        };
                        _context.Grades.Add(newGrade);
                    }
                }

                
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("ManageGrades", new { courseId = grades.Values.First().CourseId });
        }

        // ترقية الطلاب بعد اجتيازهم
        public IActionResult UpgradeStudents(int courseId)
        {
            if (!SessionHelpercs.IsTeacher(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }

            double passingGrade = 50;

            var studentsToUpgrade = _context.Grades
                .Where(g => g.CourseId == courseId)
                .Where(g => (g.TotalGrade) >= passingGrade)
                .Select(g => g.StudentId)
                .Distinct()
                .ToList();

            var students = _context.Students
                .Where(s => studentsToUpgrade.Contains(s.StudentId))
                .ToList();

            foreach (var student in students)
            {
                if (student.StudentClass != null &&
                    student.StudentClass.ClassName.StartsWith("Grade") &&
                    int.TryParse(student.StudentClass.ClassName.Split(' ')[1], out int currentClassNumber))
                {
                    int nextClassNumber = currentClassNumber + 1;
                    string nextClassName = "Grade " + nextClassNumber;

                    // البحث عن الصف التالي في جدول Classes
                    var nextClass = _context.Classes.FirstOrDefault(c => c.ClassName == nextClassName);

                    if (nextClass != null)
                    {
                    
                        student.StudentClass.ClassName = nextClassName;
                        student.ClassId = nextClass.ClassId;
                    }
                }
            }

            _context.SaveChanges();

            return RedirectToAction("ManageGrades", new { courseId });
        }

    }
}