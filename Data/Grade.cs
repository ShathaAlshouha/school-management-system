using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class Grade

    {

        [Key]

        public int GradeId { get; set; }

        [Required]

        public int? MidExam { get; set; }

        [Required]

        public int? ActivityMark { get; set; }

        [Required]

        public int? FinalExam { get; set; }

        [Required]

        public string? Semester { get; set; }

        [Required]

        public int? Years { get; set; }



        [ForeignKey(nameof(GradeStudent))]

        [Required]

        public int? StudentId { get; set; }

        public Student? GradeStudent { get; set; }



        [ForeignKey(nameof(GradeCourse))]

        [Required]

        public int? CourseId { get; set; }

        public Course? GradeCourse { get; set; }

    }
}
