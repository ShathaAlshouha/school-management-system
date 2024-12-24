using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class Teacher

    {

        [Key]

        public int TeacherId { get; set; }

        [Required]

        public string? SubjectSpecialty { get; set; }



        public DateTime? EmploymentStartDate { get; set; }

        [Required]

        public float? Salary { get; set; }



        [ForeignKey(nameof(TeacherPerson))]

        [Required]

        public int? PersonId { get; set; }

        public Person? TeacherPerson { get; set; }



        public List<TeacherAttendance>? TeacherAttendances { get; set; }

        public List<Course>? course { get; set; }

    }


}
