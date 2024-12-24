using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class Person
    {

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

        public Parent? Parent { get; set; }

        public Student? Student { get; set; }   

        public Teacher? Teacher { get; set; }

        public Staff? Staff { get; set; }

        public User? User { get; set; }
    }
}
