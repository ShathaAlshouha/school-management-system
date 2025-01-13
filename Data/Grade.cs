using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class Grade
    {

        [Key]

        public int GradeId { get; set; }

        public int? FirstExam{ get; set; }

        public int? ActivityMark { get; set; }
        public int? FinalExam { get; set; }
        public int? TotalGrade { get; set; }

        [ForeignKey(nameof(GradeStudent))]

        [Required]
        public int? StudentId { get; set; }

        public Student? GradeStudent { get; set; }

        [ForeignKey(nameof(GradeCourse))]

        [Required]
      public int CourseId { get; set; }

        public Course? GradeCourse { get; set; }

    }
}
