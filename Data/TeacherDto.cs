using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class TeacherDto
    {
        [Key]

        public int TeacherId { get; set; }

        [Required]

        public string? SubjectSpecialty { get; set; }



        public DateTime? EmploymentStartDate { get; set; } = DateTime.Now;

        [Required]

        public float? Salary { get; set; }
        [Key]
        public int PersonID { get; set; }

        [Required]

        public int? NationalID { get; set; }

        [Required]

        public string? FirstName { get; set; }

        [Required]

        public string? SecondName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public int? contactNumber { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]

        public DateTime? DateOfBirth { get; set; }

        [Required]

        public string? Address { get; set; }
        public string? Role { get; set; }
       

    }
}
