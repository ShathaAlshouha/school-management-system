using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class StudentDto
    {
        [Key]
        public int StudentId { get; set; }
        public DateTime? EnrollmentDate { get; set; } = DateTime.Now;

        public int? NationalID { get; set; }
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public int? contactNumber { get; set; }

        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Address { get; set; }
        public string? Role { get; set; }
        [Key]
        public int ClassId { get; set; }
        public int ParentID { get; set; }
      
    }
}
