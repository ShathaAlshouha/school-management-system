using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class Staff
    {
        [Key]
        public int? StaffId { get; set; }

        [Required]

        public DateTime EmploymentStartDate { get; set; }

        [Required]
        public int? Salary { get; set; }
        [Required]

        [ForeignKey(nameof(StaffPerson))]
        public int? PersonId { get; set; }
        public Person? StaffPerson { get; set; }

    }
}
