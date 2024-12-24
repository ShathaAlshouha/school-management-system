using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class StaffDto
    {
        public int? StaffId { get; set; } 

        [Required]
        public DateTime EmploymentStartDate { get; set; } = DateTime.Now;

        [Required]
        public int? Salary { get; set; }

        public int? PersonId { get; set; } 

        [Required]
        public int? NationalID { get; set; }

        [Required]
        public string? FirstName { get; set; } 

        [Required]
        public string? SecondName { get; set; }

        [Required]
        public string? LastName { get; set; } 

        [Required]
        public int? ContactNumber { get; set; } 

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; } 

        [Required]
        public string? Address { get; set; }

        public string? Role { get; set; }


    }
}
