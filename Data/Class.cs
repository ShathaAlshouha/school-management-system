using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        public string? ClassName { get; set; }

        [Required]
        public int? fees { get; set; }

        // Foreign Key for Teacher
        [ForeignKey(nameof(ClassTeacher))]
        [Required]
        public int? TeacherId { get; set; }

        public Teacher? ClassTeacher { get; set; }

        // Navigation Property for Students
        public List<Student>? Students { get; set; }

        // Navigation Property for Courses
        public List<Course>? Courses { get; set; }


        public List<Payment>? Payments { get; set; }
    }
}
