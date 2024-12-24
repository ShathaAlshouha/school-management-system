using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class StudentDto
    {
        [Key]
        public int StudentId { get; set; }
        [Required]

        public DateTime? EnrollmentDate { get; set; } = DateTime.Now; 
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

        [Required]
   
        [Key]
      
        public int ClassId { get; set; }
        [Required]
        
        public int ParentID { get; set; }

    }
}
