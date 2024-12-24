using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class Course

    {

        [Key]

        public int CourseId { get; set; }

        [Required]

        public string? CourseName { get; set; }





        [ForeignKey(nameof(CourseTeacher))]

        public int? TeacherId { get; set; }

        public Teacher? CourseTeacher { get; set; }





        [ForeignKey(nameof(CourseClass))]

        public int? ClassId { get; set; }

        public Class? CourseClass { get; set; }

        public List<Grade>? Grades { get; set; }





    }
}
