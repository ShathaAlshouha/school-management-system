using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class GradeDto
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? FirstExam { get; set; }
        public int? ActivityMark { get; set; }
        public int? FinalExam { get; set; }
        public int? TotalGrade { get; set; }
        public int CourseId { get; set; } // لتحديد المادة
    }
}
