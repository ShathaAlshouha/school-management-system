using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class UserDto
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
        public DateTime CreatedOnDate { get; set; }
        [Required]
        public int? PersonId { get; set; }
        [Key]
        public int PersonID { get; set; }

        public int? Salary { get; set; }
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
        public string? Email { get; set; }

        [Required]

        public DateTime? DateOfBirth { get; set; }

        [Required]

        public string? Address { get; set; }

        public string? Role { get; set; }

    }
}
